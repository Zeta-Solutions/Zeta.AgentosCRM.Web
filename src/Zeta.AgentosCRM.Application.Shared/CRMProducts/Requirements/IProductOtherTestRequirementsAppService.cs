using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    public interface IProductOtherTestRequirementsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductOtherTestRequirementForViewDto>> GetAll(GetAllProductOtherTestRequirementsInput input);

        Task<GetProductOtherTestRequirementForViewDto> GetProductOtherTestRequirementForView(int id);

        Task<GetProductOtherTestRequirementForEditOutput> GetProductOtherTestRequirementForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductOtherTestRequirementDto input);

        Task Delete(EntityDto input);

        Task<List<ProductOtherTestRequirementProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}