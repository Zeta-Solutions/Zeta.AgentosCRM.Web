using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos;

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    public interface IProductFeesAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductFeeForViewDto>> GetAll(GetAllProductFeesInput input);

        Task<GetProductFeeForViewDto> GetProductFeeForView(int id);

        Task<GetProductFeeForEditOutput> GetProductFeeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductFeeDto input);

        Task Delete(EntityDto input);

        Task<List<ProductFeeCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<ProductFeeInstallmentTypeLookupTableDto>> GetAllInstallmentTypeForTableDropdown();

        Task<List<ProductFeeFeeTypeLookupTableDto>> GetAllFeeTypeForTableDropdown();

        Task<List<ProductFeeProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}