using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMSetup.Documents;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners)]
    public class DocumentCheckListPartnersAppService : AgentosCRMAppServiceBase, IDocumentCheckListPartnersAppService
    {
        private readonly IRepository<DocumentCheckListPartner> _documentCheckListPartnerRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<WorkflowStepDocumentCheckList, int> _lookup_workflowStepDocumentCheckListRepository;

        public DocumentCheckListPartnersAppService(IRepository<DocumentCheckListPartner> documentCheckListPartnerRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<WorkflowStepDocumentCheckList, int> lookup_workflowStepDocumentCheckListRepository)
        {
            _documentCheckListPartnerRepository = documentCheckListPartnerRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_workflowStepDocumentCheckListRepository = lookup_workflowStepDocumentCheckListRepository;

        }

        public async Task<PagedResultDto<GetDocumentCheckListPartnerForViewDto>> GetAll(GetAllDocumentCheckListPartnersInput input)
        {

            var filteredDocumentCheckListPartners = _documentCheckListPartnerRepository.GetAll()
                        .Include(e => e.PartnerFk)
                        .Include(e => e.WorkflowStepDocumentCheckListFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowStepDocumentCheckListNameFilter), e => e.WorkflowStepDocumentCheckListFk != null && e.WorkflowStepDocumentCheckListFk.Name == input.WorkflowStepDocumentCheckListNameFilter);

            var pagedAndFilteredDocumentCheckListPartners = filteredDocumentCheckListPartners
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentCheckListPartners = from o in pagedAndFilteredDocumentCheckListPartners
                                            join o1 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o1.Id into j1
                                            from s1 in j1.DefaultIfEmpty()

                                            join o2 in _lookup_workflowStepDocumentCheckListRepository.GetAll() on o.WorkflowStepDocumentCheckListId equals o2.Id into j2
                                            from s2 in j2.DefaultIfEmpty()

                                            select new
                                            {

                                                o.Name,
                                                Id = o.Id,
                                                PartnerPartnerName = s1 == null || s1.PartnerName == null ? "" : s1.PartnerName.ToString(),
                                                WorkflowStepDocumentCheckListName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                            };

            var totalCount = await filteredDocumentCheckListPartners.CountAsync();

            var dbList = await documentCheckListPartners.ToListAsync();
            var results = new List<GetDocumentCheckListPartnerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDocumentCheckListPartnerForViewDto()
                {
                    DocumentCheckListPartner = new DocumentCheckListPartnerDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    PartnerPartnerName = o.PartnerPartnerName,
                    WorkflowStepDocumentCheckListName = o.WorkflowStepDocumentCheckListName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDocumentCheckListPartnerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetDocumentCheckListPartnerForViewDto> GetDocumentCheckListPartnerForView(int id)
        {
            var documentCheckListPartner = await _documentCheckListPartnerRepository.GetAsync(id);

            var output = new GetDocumentCheckListPartnerForViewDto { DocumentCheckListPartner = ObjectMapper.Map<DocumentCheckListPartnerDto>(documentCheckListPartner) };

            if (output.DocumentCheckListPartner.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.DocumentCheckListPartner.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.DocumentCheckListPartner.WorkflowStepDocumentCheckListId != null)
            {
                var _lookupWorkflowStepDocumentCheckList = await _lookup_workflowStepDocumentCheckListRepository.FirstOrDefaultAsync((int)output.DocumentCheckListPartner.WorkflowStepDocumentCheckListId);
                output.WorkflowStepDocumentCheckListName = _lookupWorkflowStepDocumentCheckList?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Edit)]
        public async Task<GetDocumentCheckListPartnerForEditOutput> GetDocumentCheckListPartnerForEdit(EntityDto input)
        {
            var documentCheckListPartner = await _documentCheckListPartnerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentCheckListPartnerForEditOutput { DocumentCheckListPartner = ObjectMapper.Map<CreateOrEditDocumentCheckListPartnerDto>(documentCheckListPartner) };

            if (output.DocumentCheckListPartner.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.DocumentCheckListPartner.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.DocumentCheckListPartner.WorkflowStepDocumentCheckListId != null)
            {
                var _lookupWorkflowStepDocumentCheckList = await _lookup_workflowStepDocumentCheckListRepository.FirstOrDefaultAsync((int)output.DocumentCheckListPartner.WorkflowStepDocumentCheckListId);
                output.WorkflowStepDocumentCheckListName = _lookupWorkflowStepDocumentCheckList?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentCheckListPartnerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Create)]
        protected virtual async Task Create(CreateOrEditDocumentCheckListPartnerDto input)
        {
            var documentCheckListPartner = ObjectMapper.Map<DocumentCheckListPartner>(input);

            if (AbpSession.TenantId != null)
            {
                documentCheckListPartner.TenantId = (int)AbpSession.TenantId;
            }

            await _documentCheckListPartnerRepository.InsertAsync(documentCheckListPartner);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentCheckListPartnerDto input)
        {
            var documentCheckListPartner = await _documentCheckListPartnerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentCheckListPartner);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentCheckListPartnerRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners)]
        public async Task<List<DocumentCheckListPartnerPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new DocumentCheckListPartnerPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners)]
        public async Task<List<DocumentCheckListPartnerWorkflowStepDocumentCheckListLookupTableDto>> GetAllWorkflowStepDocumentCheckListForTableDropdown()
        {
            return await _lookup_workflowStepDocumentCheckListRepository.GetAll()
                .Select(workflowStepDocumentCheckList => new DocumentCheckListPartnerWorkflowStepDocumentCheckListLookupTableDto
                {
                    Id = workflowStepDocumentCheckList.Id,
                    DisplayName = workflowStepDocumentCheckList == null || workflowStepDocumentCheckList.Name == null ? "" : workflowStepDocumentCheckList.Name.ToString()
                }).ToListAsync();
        }

    }
}