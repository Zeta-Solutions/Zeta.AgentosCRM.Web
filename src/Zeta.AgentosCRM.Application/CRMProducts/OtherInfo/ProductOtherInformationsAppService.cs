using Zeta.AgentosCRM.CRMSetup;  
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore; 

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo
{
    [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations)]
    public class ProductOtherInformationsAppService : AgentosCRMAppServiceBase, IProductOtherInformationsAppService
    {
        private readonly IRepository<ProductOtherInformation> _productOtherInformationRepository;
        private readonly IRepository<SubjectArea, int> _lookup_subjectAreaRepository;
        private readonly IRepository<Subject, int> _lookup_subjectRepository;
        private readonly IRepository<DegreeLevel, int> _lookup_degreeLevelRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ProductOtherInformationsAppService(IRepository<ProductOtherInformation> productOtherInformationRepository, IRepository<SubjectArea, int> lookup_subjectAreaRepository, IRepository<Subject, int> lookup_subjectRepository, IRepository<DegreeLevel, int> lookup_degreeLevelRepository, IRepository<Product, long> lookup_productRepository)
        {
            _productOtherInformationRepository = productOtherInformationRepository;
            _lookup_subjectAreaRepository = lookup_subjectAreaRepository;
            _lookup_subjectRepository = lookup_subjectRepository;
            _lookup_degreeLevelRepository = lookup_degreeLevelRepository;
            _lookup_productRepository = lookup_productRepository;
        }

        public async Task<PagedResultDto<GetProductOtherInformationForViewDto>> GetAll(GetAllProductOtherInformationsInput input)
        {

            var filteredProductOtherInformations = _productOtherInformationRepository.GetAll()
                        .Include(e => e.SubjectAreaFk)
                        .Include(e => e.SubjectFk)
                        .Include(e => e.DegreeLevelFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectAreaNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.DegreeLevelFk != null && e.DegreeLevelFk.Name == input.DegreeLevelNameFilter);

            var pagedAndFilteredProductOtherInformations = filteredProductOtherInformations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productOtherInformations = from o in pagedAndFilteredProductOtherInformations
                                           join o1 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o1.Id into j1
                                           from s1 in j1.DefaultIfEmpty()

                                           join o2 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o2.Id into j2
                                           from s2 in j2.DefaultIfEmpty()

                                           join o3 in _lookup_degreeLevelRepository.GetAll() on o.DegreeLevelId equals o3.Id into j3
                                           from s3 in j3.DefaultIfEmpty()

                                           select new
                                           {

                                               o.Name,
                                               Id = o.Id,
                                               SubjectAreaName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                               SubjectName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                               DegreeLevelName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                                           };

            var totalCount = await filteredProductOtherInformations.CountAsync();

            var dbList = await productOtherInformations.ToListAsync();
            var results = new List<GetProductOtherInformationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductOtherInformationForViewDto()
                {
                    ProductOtherInformation = new ProductOtherInformationDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    SubjectAreaName = o.SubjectAreaName,
                    SubjectName = o.SubjectName,
                    DegreeLevelName = o.DegreeLevelName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductOtherInformationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductOtherInformationForViewDto> GetProductOtherInformationForView(int id)
        {
            var productOtherInformation = await _productOtherInformationRepository.GetAsync(id);

            var output = new GetProductOtherInformationForViewDto { ProductOtherInformation = ObjectMapper.Map<ProductOtherInformationDto>(productOtherInformation) };

            if (output.ProductOtherInformation.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.ProductOtherInformation.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.ProductOtherInformation.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations_Edit)]
        public async Task<GetProductOtherInformationForEditOutput> GetProductOtherInformationForEdit(EntityDto input)
        {
            var productOtherInformation = await _productOtherInformationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductOtherInformationForEditOutput { ProductOtherInformation = ObjectMapper.Map<CreateOrEditProductOtherInformationDto>(productOtherInformation) };

            if (output.ProductOtherInformation.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.ProductOtherInformation.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.ProductOtherInformation.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ProductOtherInformation.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductOtherInformationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations_Create)]
        protected virtual async Task Create(CreateOrEditProductOtherInformationDto input)
        {
            var productOtherInformation = ObjectMapper.Map<ProductOtherInformation>(input);

            if (AbpSession.TenantId != null)
            {
                productOtherInformation.TenantId = (int)AbpSession.TenantId;
            }

            await _productOtherInformationRepository.InsertAsync(productOtherInformation);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations_Edit)]
        protected virtual async Task Update(CreateOrEditProductOtherInformationDto input)
        {
            var productOtherInformation = await _productOtherInformationRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productOtherInformation);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productOtherInformationRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations)]
        public async Task<List<ProductOtherInformationSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown()
        {
            return await _lookup_subjectAreaRepository.GetAll()
                .Select(subjectArea => new ProductOtherInformationSubjectAreaLookupTableDto
                {
                    Id = subjectArea.Id,
                    DisplayName = subjectArea == null || subjectArea.Name == null ? "" : subjectArea.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations)]
        public async Task<List<ProductOtherInformationSubjectLookupTableDto>> GetAllSubjectForTableDropdown()
        {
            return await _lookup_subjectRepository.GetAll()
                .Select(subject => new ProductOtherInformationSubjectLookupTableDto
                {
                    Id = subject.Id,
                    DisplayName = subject == null || subject.Name == null ? "" : subject.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations)]
        public async Task<PagedResultDto<ProductOtherInformationDegreeLevelLookupTableDto>> GetAllDegreeLevelForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_degreeLevelRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name != null && e.Name.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var degreeLevelList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ProductOtherInformationDegreeLevelLookupTableDto>();
            foreach (var degreeLevel in degreeLevelList)
            {
                lookupTableDtoList.Add(new ProductOtherInformationDegreeLevelLookupTableDto
                {
                    Id = degreeLevel.Id,
                    DisplayName = degreeLevel.Name?.ToString()
                });
            }

            return new PagedResultDto<ProductOtherInformationDegreeLevelLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherInformations)]
        public async Task<List<ProductOtherInformationProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductOtherInformationProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}