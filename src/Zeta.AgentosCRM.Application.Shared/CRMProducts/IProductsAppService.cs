using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic; 

namespace Zeta.AgentosCRM.CRMProducts
{
    public interface IProductsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductForViewDto>> GetAll(GetAllProductsInput input);

        Task<List<GetProductForViewDto>> GetProductsByPartnerId(List<long> partnerIds); 

        Task<GetProductForViewDto> GetProductForView(long id); 

        Task<GetProductForEditOutput> GetProductForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditProductDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetProductsToExcel(GetAllProductsForExcelInput input);

        Task<List<ProductPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<ProductPartnerTypeLookupTableDto>> GetAllPartnerTypeForTableDropdown();

        Task<List<ProductBranchLookupTableDto>> GetAllBranchForTableDropdown();

    }
}