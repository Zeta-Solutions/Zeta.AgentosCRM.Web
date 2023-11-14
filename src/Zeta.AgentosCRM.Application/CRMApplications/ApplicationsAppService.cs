using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMProducts;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMApplications.Exporting;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMApplications
{
    [AbpAuthorize(AppPermissions.Pages_Applications)]
    public class ApplicationsAppService : AgentosCRMAppServiceBase, IApplicationsAppService
    {
        private readonly IRepository<Application, long> _applicationRepository;
        private readonly IApplicationsExcelExporter _applicationsExcelExporter;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ApplicationsAppService(IRepository<Application, long> applicationRepository, IApplicationsExcelExporter applicationsExcelExporter, IRepository<Client, long> lookup_clientRepository, IRepository<Workflow, int> lookup_workflowRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<Product, long> lookup_productRepository)
        {
            _applicationRepository = applicationRepository;
            _applicationsExcelExporter = applicationsExcelExporter;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_workflowRepository = lookup_workflowRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_productRepository = lookup_productRepository;

        }

        public async Task<PagedResultDto<GetApplicationForViewDto>> GetAll(GetAllApplicationsInput input)
        {

            var filteredApplications = _applicationRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.WorkflowFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.ProductFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter);

            var pagedAndFilteredApplications = filteredApplications
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var applications = from o in pagedAndFilteredApplications
                               join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               join o2 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o2.Id into j2
                               from s2 in j2.DefaultIfEmpty()

                               join o3 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o3.Id into j3
                               from s3 in j3.DefaultIfEmpty()

                               join o4 in _lookup_productRepository.GetAll() on o.ProductId equals o4.Id into j4
                               from s4 in j4.DefaultIfEmpty()

                               select new
                               {

                                   Id = o.Id,
                                   ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                   WorkflowName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                   PartnerPartnerName = s3 == null || s3.PartnerName == null ? "" : s3.PartnerName.ToString(),
                                   ProductName = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                               };

            var totalCount = await filteredApplications.CountAsync();

            var dbList = await applications.ToListAsync();
            var results = new List<GetApplicationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetApplicationForViewDto()
                {
                    Application = new ApplicationDto
                    {

                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName,
                    WorkflowName = o.WorkflowName,
                    PartnerPartnerName = o.PartnerPartnerName,
                    ProductName = o.ProductName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetApplicationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetApplicationForViewDto> GetApplicationForView(long id)
        {
            var application = await _applicationRepository.GetAsync(id);

            var output = new GetApplicationForViewDto { Application = ObjectMapper.Map<ApplicationDto>(application) };

            if (output.Application.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Application.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.Application.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.Application.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.Application.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Application.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Application.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.Application.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Applications_Edit)]
        public async Task<GetApplicationForEditOutput> GetApplicationForEdit(EntityDto<long> input)
        {
            var application = await _applicationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetApplicationForEditOutput { Application = ObjectMapper.Map<CreateOrEditApplicationDto>(application) };

            if (output.Application.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Application.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.Application.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.Application.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.Application.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Application.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Application.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.Application.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditApplicationDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Applications_Create)]
        protected virtual async Task Create(CreateOrEditApplicationDto input)
        {
            var application = ObjectMapper.Map<Application>(input);

            if (AbpSession.TenantId != null)
            {
                application.TenantId = (int)AbpSession.TenantId;
            }

            await _applicationRepository.InsertAsync(application);

        }

        [AbpAuthorize(AppPermissions.Pages_Applications_Edit)]
        protected virtual async Task Update(CreateOrEditApplicationDto input)
        {
            var application = await _applicationRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, application);

        }

        [AbpAuthorize(AppPermissions.Pages_Applications_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _applicationRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetApplicationsToExcel(GetAllApplicationsForExcelInput input)
        {

            var filteredApplications = _applicationRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.WorkflowFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.ProductFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter);

            var query = (from o in filteredApplications
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_productRepository.GetAll() on o.ProductId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         select new GetApplicationForViewDto()
                         {
                             Application = new ApplicationDto
                             {
                                 Id = o.Id
                             },
                             ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                             WorkflowName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             PartnerPartnerName = s3 == null || s3.PartnerName == null ? "" : s3.PartnerName.ToString(),
                             ProductName = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                         });

            var applicationListDtos = await query.ToListAsync();

            return _applicationsExcelExporter.ExportToFile(applicationListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
        public async Task<List<ApplicationClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ApplicationClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
        public async Task<List<ApplicationWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown()
        {
            return await _lookup_workflowRepository.GetAll()
                .Select(workflow => new ApplicationWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow == null || workflow.Name == null ? "" : workflow.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
        public async Task<List<ApplicationPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new ApplicationPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
        public async Task<List<ApplicationProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ApplicationProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}