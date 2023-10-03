using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Exporting;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory
{
    [AbpAuthorize(AppPermissions.Pages_ServiceCategories)]
    public class ServiceCategoriesAppService : AgentosCRMAppServiceBase, IServiceCategoriesAppService
    {
        private readonly IRepository<ServiceCategory> _serviceCategoryRepository;
        private readonly IServiceCategoriesExcelExporter _serviceCategoriesExcelExporter;

        public ServiceCategoriesAppService(IRepository<ServiceCategory> serviceCategoryRepository, IServiceCategoriesExcelExporter serviceCategoriesExcelExporter)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
            _serviceCategoriesExcelExporter = serviceCategoriesExcelExporter;

        }

        public async Task<PagedResultDto<GetServiceCategoryForViewDto>> GetAll(GetAllServiceCategoriesInput input)
        {

            var filteredServiceCategories = _serviceCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredServiceCategories = filteredServiceCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var serviceCategories = from o in pagedAndFilteredServiceCategories
                                    select new
                                    {

                                        o.Abbrivation,
                                        o.Name,
                                        Id = o.Id
                                    };

            var totalCount = await filteredServiceCategories.CountAsync();

            var dbList = await serviceCategories.ToListAsync();
            var results = new List<GetServiceCategoryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetServiceCategoryForViewDto()
                {
                    ServiceCategory = new ServiceCategoryDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetServiceCategoryForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetServiceCategoryForViewDto> GetServiceCategoryForView(int id)
        {
            var serviceCategory = await _serviceCategoryRepository.GetAsync(id);

            var output = new GetServiceCategoryForViewDto { ServiceCategory = ObjectMapper.Map<ServiceCategoryDto>(serviceCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ServiceCategories_Edit)]
        public async Task<GetServiceCategoryForEditOutput> GetServiceCategoryForEdit(EntityDto input)
        {
            var serviceCategory = await _serviceCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetServiceCategoryForEditOutput { ServiceCategory = ObjectMapper.Map<CreateOrEditServiceCategoryDto>(serviceCategory) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditServiceCategoryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ServiceCategories_Create)]
        protected virtual async Task Create(CreateOrEditServiceCategoryDto input)
        {
            var serviceCategory = ObjectMapper.Map<ServiceCategory>(input);

            if (AbpSession.TenantId != null)
            {
                serviceCategory.TenantId = (int)AbpSession.TenantId;
            }

            await _serviceCategoryRepository.InsertAsync(serviceCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_ServiceCategories_Edit)]
        protected virtual async Task Update(CreateOrEditServiceCategoryDto input)
        {
            var serviceCategory = await _serviceCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, serviceCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_ServiceCategories_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _serviceCategoryRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetServiceCategoriesToExcel(GetAllServiceCategoriesForExcelInput input)
        {

            var filteredServiceCategories = _serviceCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var query = (from o in filteredServiceCategories
                         select new GetServiceCategoryForViewDto()
                         {
                             ServiceCategory = new ServiceCategoryDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             }
                         });

            var serviceCategoryListDtos = await query.ToListAsync();

            return _serviceCategoriesExcelExporter.ExportToFile(serviceCategoryListDtos);
        }

    }
}