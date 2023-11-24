using Abp.Organizations; 
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Dtos; 
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization; 
using Abp.Authorization;
using Microsoft.EntityFrameworkCore; 

namespace Zeta.AgentosCRM.CRMSetup
{
    [AbpAuthorize(AppPermissions.Pages_WorkflowOffices)]
    public class WorkflowOfficesAppService : AgentosCRMAppServiceBase, IWorkflowOfficesAppService
    {
        private readonly IRepository<WorkflowOffice> _workflowOfficeRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;

        public WorkflowOfficesAppService(IRepository<WorkflowOffice> workflowOfficeRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository, IRepository<Workflow, int> lookup_workflowRepository)
        {
            _workflowOfficeRepository = workflowOfficeRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
            _lookup_workflowRepository = lookup_workflowRepository;

        }

        public async Task<PagedResultDto<GetWorkflowOfficeForViewDto>> GetAll(GetAllWorkflowOfficesInput input)
        {

            var filteredWorkflowOffices = _workflowOfficeRepository.GetAll()
                        .Include(e => e.OrganizationUnitFk)
                        .Include(e => e.WorkflowFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter);

            var pagedAndFilteredWorkflowOffices = filteredWorkflowOffices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workflowOffices = from o in pagedAndFilteredWorkflowOffices
                                  join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  join o2 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o2.Id into j2
                                  from s2 in j2.DefaultIfEmpty()

                                  select new
                                  {

                                      o.Name,
                                      Id = o.Id,
                                      OrganizationUnitDisplayName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString(),
                                      WorkflowName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                  };

            var totalCount = await filteredWorkflowOffices.CountAsync();

            var dbList = await workflowOffices.ToListAsync();
            var results = new List<GetWorkflowOfficeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkflowOfficeForViewDto()
                {
                    WorkflowOffice = new WorkflowOfficeDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName,
                    WorkflowName = o.WorkflowName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkflowOfficeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkflowOfficeForViewDto> GetWorkflowOfficeForView(int id)
        {
            var workflowOffice = await _workflowOfficeRepository.GetAsync(id);

            var output = new GetWorkflowOfficeForViewDto { WorkflowOffice = ObjectMapper.Map<WorkflowOfficeDto>(workflowOffice) };

            if (output.WorkflowOffice.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.WorkflowOffice.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            if (output.WorkflowOffice.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.WorkflowOffice.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices_Edit)]
        public async Task<GetWorkflowOfficeForEditOutput> GetWorkflowOfficeForEdit(EntityDto input)
        {
            var workflowOffice = await _workflowOfficeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWorkflowOfficeForEditOutput { WorkflowOffice = ObjectMapper.Map<CreateOrEditWorkflowOfficeDto>(workflowOffice) };

            if (output.WorkflowOffice.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.WorkflowOffice.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            if (output.WorkflowOffice.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.WorkflowOffice.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkflowOfficeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices_Create)]
        protected virtual async Task Create(CreateOrEditWorkflowOfficeDto input)
        {
            var workflowOffice = ObjectMapper.Map<WorkflowOffice>(input);

            if (AbpSession.TenantId != null)
            {
                workflowOffice.TenantId = (int)AbpSession.TenantId;
            }

            await _workflowOfficeRepository.InsertAsync(workflowOffice);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices_Edit)]
        protected virtual async Task Update(CreateOrEditWorkflowOfficeDto input)
        {
            var workflowOffice = await _workflowOfficeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, workflowOffice);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _workflowOfficeRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices)]
        public async Task<List<WorkflowOfficeOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new WorkflowOfficeOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowOffices)]
        public async Task<List<WorkflowOfficeWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown()
        {
            return await _lookup_workflowRepository.GetAll()
                .Select(workflow => new WorkflowOfficeWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow == null || workflow.Name == null ? "" : workflow.Name.ToString()
                }).ToListAsync();
        }

    }
}