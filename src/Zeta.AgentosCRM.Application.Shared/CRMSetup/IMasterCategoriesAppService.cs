using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IMasterCategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetMasterCategoryForViewDto>> GetAll(GetAllMasterCategoriesInput input);

        Task<GetMasterCategoryForViewDto> GetMasterCategoryForView(int id);

        Task<GetMasterCategoryForEditOutput> GetMasterCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMasterCategoryDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetMasterCategoriesToExcel(GetAllMasterCategoriesForExcelInput input);

    }
}