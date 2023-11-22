using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    public interface IPromotionProductsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPromotionProductForViewDto>> GetAll(GetAllPromotionProductsInput input);

        Task<GetPromotionProductForViewDto> GetPromotionProductForView(long id);

        Task<GetPromotionProductForEditOutput> GetPromotionProductForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPromotionProductDto input);

        Task Delete(EntityDto<long> input);

        Task<List<PromotionProductPartnerPromotionLookupTableDto>> GetAllPartnerPromotionForTableDropdown();

        Task<List<PromotionProductProductLookupTableDto>> GetAllProductForTableDropdown(long? id);

    }
}