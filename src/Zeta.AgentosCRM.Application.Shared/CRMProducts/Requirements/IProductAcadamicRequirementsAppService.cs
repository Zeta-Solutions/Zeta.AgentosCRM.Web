using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos; 
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    public interface IProductAcadamicRequirementsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductAcadamicRequirementForViewDto>> GetAll(GetAllProductAcadamicRequirementsInput input);

        Task<GetProductAcadamicRequirementForViewDto> GetProductAcadamicRequirementForView(int id);

        Task<GetProductAcadamicRequirementForEditOutput> GetProductAcadamicRequirementForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductAcadamicRequirementDto input);

        Task Delete(EntityDto input);

        Task<List<ProductAcadamicRequirementDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown();

        Task<List<ProductAcadamicRequirementProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}