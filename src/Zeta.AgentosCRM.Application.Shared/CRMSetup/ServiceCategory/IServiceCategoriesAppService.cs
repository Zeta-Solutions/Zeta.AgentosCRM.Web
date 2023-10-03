using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory
{
    public interface IServiceCategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetServiceCategoryForViewDto>> GetAll(GetAllServiceCategoriesInput input);

        Task<GetServiceCategoryForViewDto> GetServiceCategoryForView(int id);

        Task<GetServiceCategoryForEditOutput> GetServiceCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditServiceCategoryDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetServiceCategoriesToExcel(GetAllServiceCategoriesForExcelInput input);

    }
}