using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.ProductType
{
    public interface IProductTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductTypeForViewDto>> GetAll(GetAllProductTypesInput input);

        Task<GetProductTypeForViewDto> GetProductTypeForView(int id);

        Task<GetProductTypeForEditOutput> GetProductTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductTypeDto input);

        Task Delete(EntityDto input);

        Task<List<ProductTypeMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown();

    }
}