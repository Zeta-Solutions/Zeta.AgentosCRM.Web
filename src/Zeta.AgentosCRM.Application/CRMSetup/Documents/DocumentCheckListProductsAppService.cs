using Zeta.AgentosCRM.CRMProducts;
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
    [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts)]
    public class DocumentCheckListProductsAppService : AgentosCRMAppServiceBase, IDocumentCheckListProductsAppService
    {
        private readonly IRepository<DocumentCheckListProduct> _documentCheckListProductRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;
        private readonly IRepository<WorkflowStepDocumentCheckList, int> _lookup_workflowStepDocumentCheckListRepository;

        public DocumentCheckListProductsAppService(IRepository<DocumentCheckListProduct> documentCheckListProductRepository, IRepository<Product, long> lookup_productRepository, IRepository<WorkflowStepDocumentCheckList, int> lookup_workflowStepDocumentCheckListRepository)
        {
            _documentCheckListProductRepository = documentCheckListProductRepository;
            _lookup_productRepository = lookup_productRepository;
            _lookup_workflowStepDocumentCheckListRepository = lookup_workflowStepDocumentCheckListRepository;

        }

        public async Task<PagedResultDto<GetDocumentCheckListProductForViewDto>> GetAll(GetAllDocumentCheckListProductsInput input)
        {

            var filteredDocumentCheckListProducts = _documentCheckListProductRepository.GetAll()
                        .Include(e => e.ProductFk)
                        .Include(e => e.WorkflowStepDocumentCheckListFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowStepDocumentCheckListNameFilter), e => e.WorkflowStepDocumentCheckListFk != null && e.WorkflowStepDocumentCheckListFk.Name == input.WorkflowStepDocumentCheckListNameFilter);

            var pagedAndFilteredDocumentCheckListProducts = filteredDocumentCheckListProducts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentCheckListProducts = from o in pagedAndFilteredDocumentCheckListProducts
                                            join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                                            from s1 in j1.DefaultIfEmpty()

                                            join o2 in _lookup_workflowStepDocumentCheckListRepository.GetAll() on o.WorkflowStepDocumentCheckListId equals o2.Id into j2
                                            from s2 in j2.DefaultIfEmpty()

                                            select new
                                            {

                                                o.Name,
                                                Id = o.Id,
                                                ProductName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                                WorkflowStepDocumentCheckListName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                            };

            var totalCount = await filteredDocumentCheckListProducts.CountAsync();

            var dbList = await documentCheckListProducts.ToListAsync();
            var results = new List<GetDocumentCheckListProductForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDocumentCheckListProductForViewDto()
                {
                    DocumentCheckListProduct = new DocumentCheckListProductDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    ProductName = o.ProductName,
                    WorkflowStepDocumentCheckListName = o.WorkflowStepDocumentCheckListName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDocumentCheckListProductForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetDocumentCheckListProductForViewDto> GetDocumentCheckListProductForView(int id)
        {
            var documentCheckListProduct = await _documentCheckListProductRepository.GetAsync(id);

            var output = new GetDocumentCheckListProductForViewDto { DocumentCheckListProduct = ObjectMapper.Map<DocumentCheckListProductDto>(documentCheckListProduct) };

            if (output.DocumentCheckListProduct.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.DocumentCheckListProduct.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.DocumentCheckListProduct.WorkflowStepDocumentCheckListId != null)
            {
                var _lookupWorkflowStepDocumentCheckList = await _lookup_workflowStepDocumentCheckListRepository.FirstOrDefaultAsync((int)output.DocumentCheckListProduct.WorkflowStepDocumentCheckListId);
                output.WorkflowStepDocumentCheckListName = _lookupWorkflowStepDocumentCheckList?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts_Edit)]
        public async Task<GetDocumentCheckListProductForEditOutput> GetDocumentCheckListProductForEdit(EntityDto input)
        {
            var documentCheckListProduct = await _documentCheckListProductRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentCheckListProductForEditOutput { DocumentCheckListProduct = ObjectMapper.Map<CreateOrEditDocumentCheckListProductDto>(documentCheckListProduct) };

            if (output.DocumentCheckListProduct.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.DocumentCheckListProduct.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.DocumentCheckListProduct.WorkflowStepDocumentCheckListId != null)
            {
                var _lookupWorkflowStepDocumentCheckList = await _lookup_workflowStepDocumentCheckListRepository.FirstOrDefaultAsync((int)output.DocumentCheckListProduct.WorkflowStepDocumentCheckListId);
                output.WorkflowStepDocumentCheckListName = _lookupWorkflowStepDocumentCheckList?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentCheckListProductDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts_Create)]
        protected virtual async Task Create(CreateOrEditDocumentCheckListProductDto input)
        {
            var documentCheckListProduct = ObjectMapper.Map<DocumentCheckListProduct>(input);

            if (AbpSession.TenantId != null)
            {
                documentCheckListProduct.TenantId = (int)AbpSession.TenantId;
            }

            await _documentCheckListProductRepository.InsertAsync(documentCheckListProduct);

        }

        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentCheckListProductDto input)
        {
            var documentCheckListProduct = await _documentCheckListProductRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentCheckListProduct);

        }

        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentCheckListProductRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts)]
        public async Task<List<DocumentCheckListProductProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new DocumentCheckListProductProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentCheckListProducts)]
        public async Task<List<DocumentCheckListProductWorkflowStepDocumentCheckListLookupTableDto>> GetAllWorkflowStepDocumentCheckListForTableDropdown()
        {
            return await _lookup_workflowStepDocumentCheckListRepository.GetAll()
                .Select(workflowStepDocumentCheckList => new DocumentCheckListProductWorkflowStepDocumentCheckListLookupTableDto
                {
                    Id = workflowStepDocumentCheckList.Id,
                    DisplayName = workflowStepDocumentCheckList == null || workflowStepDocumentCheckList.Name == null ? "" : workflowStepDocumentCheckList.Name.ToString()
                }).ToListAsync();
        }

    }
}