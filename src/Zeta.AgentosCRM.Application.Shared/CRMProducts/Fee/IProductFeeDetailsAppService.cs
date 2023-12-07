using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    public interface IProductFeeDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductFeeDetailForViewDto>> GetAll(GetAllProductFeeDetailsInput input);

        Task<GetProductFeeDetailForViewDto> GetProductFeeDetailForView(long id);

        Task<GetProductFeeDetailForEditOutput> GetProductFeeDetailForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditProductFeeDetailDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetProductFeeDetailsToExcel(GetAllProductFeeDetailsForExcelInput input);

        Task<List<ProductFeeDetailFeeTypeLookupTableDto>> GetAllFeeTypeForTableDropdown();

        Task<List<ProductFeeDetailProductFeeLookupTableDto>> GetAllProductFeeForTableDropdown();

    }
}