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
    [AbpAuthorize(AppPermissions.Pages_DocumentTypes)]
    public class DocumentTypesAppService : AgentosCRMAppServiceBase, IDocumentTypesAppService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;

        public DocumentTypesAppService(IRepository<DocumentType> documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;

        }

        public async Task<PagedResultDto<GetDocumentTypeForViewDto>> GetAll(GetAllDocumentTypesInput input)
        {

            var filteredDocumentTypes = _documentTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredDocumentTypes = filteredDocumentTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documentTypes = from o in pagedAndFilteredDocumentTypes
                                select new
                                {

                                    o.Abbrivation,
                                    o.Name,
                                    Id = o.Id
                                };

            var totalCount = await filteredDocumentTypes.CountAsync();

            var dbList = await documentTypes.ToListAsync();
            var results = new List<GetDocumentTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDocumentTypeForViewDto()
                {
                    DocumentType = new DocumentTypeDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDocumentTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetDocumentTypeForViewDto> GetDocumentTypeForView(int id)
        {
            var documentType = await _documentTypeRepository.GetAsync(id);

            var output = new GetDocumentTypeForViewDto { DocumentType = ObjectMapper.Map<DocumentTypeDto>(documentType) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Edit)]
        public async Task<GetDocumentTypeForEditOutput> GetDocumentTypeForEdit(EntityDto input)
        {
            var documentType = await _documentTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentTypeForEditOutput { DocumentType = ObjectMapper.Map<CreateOrEditDocumentTypeDto>(documentType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Create)]
        protected virtual async Task Create(CreateOrEditDocumentTypeDto input)
        {
            var documentType = ObjectMapper.Map<DocumentType>(input);

            if (AbpSession.TenantId != null)
            {
                documentType.TenantId = (int)AbpSession.TenantId;
            }

            await _documentTypeRepository.InsertAsync(documentType);

        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Edit)]
        protected virtual async Task Update(CreateOrEditDocumentTypeDto input)
        {
            var documentType = await _documentTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, documentType);

        }

        [AbpAuthorize(AppPermissions.Pages_DocumentTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _documentTypeRepository.DeleteAsync(input.Id);
        }

    }
}