using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts
{
    public interface IProductBranchesAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductBranchForViewDto>> GetAll(GetAllProductBranchesInput input);

        Task<GetProductBranchForViewDto> GetProductBranchForView(int id);

        Task<GetProductBranchForEditOutput> GetProductBranchForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductBranchDto input);

        Task Delete(EntityDto input);

        Task<List<ProductBranchProductLookupTableDto>> GetAllProductForTableDropdown();

        Task<List<ProductBranchBranchLookupTableDto>> GetAllBranchForTableDropdown();

    }
}