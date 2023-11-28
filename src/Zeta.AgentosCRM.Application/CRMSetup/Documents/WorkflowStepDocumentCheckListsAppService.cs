﻿using Zeta.AgentosCRM.CRMSetup;
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
    [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists)]
    public class WorkflowStepDocumentCheckListsAppService : AgentosCRMAppServiceBase, IWorkflowStepDocumentCheckListsAppService
    {
        private readonly IRepository<WorkflowStepDocumentCheckList> _workflowStepDocumentCheckListRepository;
        private readonly IRepository<WorkflowStep, int> _lookup_workflowStepRepository;
        private readonly IRepository<DocumentType, int> _lookup_documentTypeRepository;

        public WorkflowStepDocumentCheckListsAppService(IRepository<WorkflowStepDocumentCheckList> workflowStepDocumentCheckListRepository, IRepository<WorkflowStep, int> lookup_workflowStepRepository, IRepository<DocumentType, int> lookup_documentTypeRepository)
        {
            _workflowStepDocumentCheckListRepository = workflowStepDocumentCheckListRepository;
            _lookup_workflowStepRepository = lookup_workflowStepRepository;
            _lookup_documentTypeRepository = lookup_documentTypeRepository;

        }

        public async Task<PagedResultDto<GetWorkflowStepDocumentCheckListForViewDto>> GetAll(GetAllWorkflowStepDocumentCheckListsInput input)
        {

            var filteredWorkflowStepDocumentCheckLists = _workflowStepDocumentCheckListRepository.GetAll()
                        .Include(e => e.WorkflowStepFk)
                        .Include(e => e.DocumentTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.IsForAllPartnersFilter.HasValue && input.IsForAllPartnersFilter > -1, e => (input.IsForAllPartnersFilter == 1 && e.IsForAllPartners) || (input.IsForAllPartnersFilter == 0 && !e.IsForAllPartners))
                        .WhereIf(input.IsForAllProductsFilter.HasValue && input.IsForAllProductsFilter > -1, e => (input.IsForAllProductsFilter == 1 && e.IsForAllProducts) || (input.IsForAllProductsFilter == 0 && !e.IsForAllProducts))
                        .WhereIf(input.AllowOnClientPortalFilter.HasValue && input.AllowOnClientPortalFilter > -1, e => (input.AllowOnClientPortalFilter == 1 && e.AllowOnClientPortal) || (input.AllowOnClientPortalFilter == 0 && !e.AllowOnClientPortal))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowStepNameFilter), e => e.WorkflowStepFk != null && e.WorkflowStepFk.Name == input.WorkflowStepNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DocumentTypeNameFilter), e => e.DocumentTypeFk != null && e.DocumentTypeFk.Name == input.DocumentTypeNameFilter);

            var pagedAndFilteredWorkflowStepDocumentCheckLists = filteredWorkflowStepDocumentCheckLists
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workflowStepDocumentCheckLists = from o in pagedAndFilteredWorkflowStepDocumentCheckLists
                                                 join o1 in _lookup_workflowStepRepository.GetAll() on o.WorkflowStepId equals o1.Id into j1
                                                 from s1 in j1.DefaultIfEmpty()

                                                 join o2 in _lookup_documentTypeRepository.GetAll() on o.DocumentTypeId equals o2.Id into j2
                                                 from s2 in j2.DefaultIfEmpty()

                                                 select new
                                                 {

                                                     o.Name,
                                                     o.Description,
                                                     o.IsForAllPartners,
                                                     o.IsForAllProducts,
                                                     o.AllowOnClientPortal,
                                                     Id = o.Id,
                                                     WorkflowStepName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                                     DocumentTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                                 };

            var totalCount = await filteredWorkflowStepDocumentCheckLists.CountAsync();

            var dbList = await workflowStepDocumentCheckLists.ToListAsync();
            var results = new List<GetWorkflowStepDocumentCheckListForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkflowStepDocumentCheckListForViewDto()
                {
                    WorkflowStepDocumentCheckList = new WorkflowStepDocumentCheckListDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        IsForAllPartners = o.IsForAllPartners,
                        IsForAllProducts = o.IsForAllProducts,
                        AllowOnClientPortal = o.AllowOnClientPortal,
                        Id = o.Id,
                    },
                    WorkflowStepName = o.WorkflowStepName,
                    DocumentTypeName = o.DocumentTypeName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkflowStepDocumentCheckListForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkflowStepDocumentCheckListForViewDto> GetWorkflowStepDocumentCheckListForView(int id)
        {
            var workflowStepDocumentCheckList = await _workflowStepDocumentCheckListRepository.GetAsync(id);

            var output = new GetWorkflowStepDocumentCheckListForViewDto { WorkflowStepDocumentCheckList = ObjectMapper.Map<WorkflowStepDocumentCheckListDto>(workflowStepDocumentCheckList) };

            if (output.WorkflowStepDocumentCheckList.WorkflowStepId != null)
            {
                var _lookupWorkflowStep = await _lookup_workflowStepRepository.FirstOrDefaultAsync((int)output.WorkflowStepDocumentCheckList.WorkflowStepId);
                output.WorkflowStepName = _lookupWorkflowStep?.Name?.ToString();
            }

            if (output.WorkflowStepDocumentCheckList.DocumentTypeId != null)
            {
                var _lookupDocumentType = await _lookup_documentTypeRepository.FirstOrDefaultAsync((int)output.WorkflowStepDocumentCheckList.DocumentTypeId);
                output.DocumentTypeName = _lookupDocumentType?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists_Edit)]
        public async Task<GetWorkflowStepDocumentCheckListForEditOutput> GetWorkflowStepDocumentCheckListForEdit(EntityDto input)
        {
            var workflowStepDocumentCheckList = await _workflowStepDocumentCheckListRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWorkflowStepDocumentCheckListForEditOutput { WorkflowStepDocumentCheckList = ObjectMapper.Map<CreateOrEditWorkflowStepDocumentCheckListDto>(workflowStepDocumentCheckList) };

            if (output.WorkflowStepDocumentCheckList.WorkflowStepId != null)
            {
                var _lookupWorkflowStep = await _lookup_workflowStepRepository.FirstOrDefaultAsync((int)output.WorkflowStepDocumentCheckList.WorkflowStepId);
                output.WorkflowStepName = _lookupWorkflowStep?.Name?.ToString();
            }

            if (output.WorkflowStepDocumentCheckList.DocumentTypeId != null)
            {
                var _lookupDocumentType = await _lookup_documentTypeRepository.FirstOrDefaultAsync((int)output.WorkflowStepDocumentCheckList.DocumentTypeId);
                output.DocumentTypeName = _lookupDocumentType?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkflowStepDocumentCheckListDto input)
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

        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists_Create)]
        protected virtual async Task Create(CreateOrEditWorkflowStepDocumentCheckListDto input)
        {
            var workflowStepDocumentCheckList = ObjectMapper.Map<WorkflowStepDocumentCheckList>(input);

            if (AbpSession.TenantId != null)
            {
                workflowStepDocumentCheckList.TenantId = (int)AbpSession.TenantId;
            }

            await _workflowStepDocumentCheckListRepository.InsertAsync(workflowStepDocumentCheckList);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists_Edit)]
        protected virtual async Task Update(CreateOrEditWorkflowStepDocumentCheckListDto input)
        {
            var workflowStepDocumentCheckList = await _workflowStepDocumentCheckListRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, workflowStepDocumentCheckList);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _workflowStepDocumentCheckListRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists)]
        public async Task<List<WorkflowStepDocumentCheckListWorkflowStepLookupTableDto>> GetAllWorkflowStepForTableDropdown()
        {
            return await _lookup_workflowStepRepository.GetAll()
                .Select(workflowStep => new WorkflowStepDocumentCheckListWorkflowStepLookupTableDto
                {
                    Id = workflowStep.Id,
                    DisplayName = workflowStep == null || workflowStep.Name == null ? "" : workflowStep.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists)]
        public async Task<List<WorkflowStepDocumentCheckListDocumentTypeLookupTableDto>> GetAllDocumentTypeForTableDropdown()
        {
            return await _lookup_documentTypeRepository.GetAll()
                .Select(documentType => new WorkflowStepDocumentCheckListDocumentTypeLookupTableDto
                {
                    Id = documentType.Id,
                    DisplayName = documentType == null || documentType.Name == null ? "" : documentType.Name.ToString()
                }).ToListAsync();
        }

    }
}