using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    public interface IProductEnglishRequirementsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductEnglishRequirementForViewDto>> GetAll(GetAllProductEnglishRequirementsInput input);

        Task<GetProductEnglishRequirementForViewDto> GetProductEnglishRequirementForView(int id);

        Task<GetProductEnglishRequirementForEditOutput> GetProductEnglishRequirementForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductEnglishRequirementDto input);

        Task Delete(EntityDto input);

        Task<List<ProductEnglishRequirementProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}