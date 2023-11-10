using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    public interface IPartnerPromotionsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPartnerPromotionForViewDto>> GetAll(GetAllPartnerPromotionsInput input);

        Task<GetPartnerPromotionForViewDto> GetPartnerPromotionForView(long id);

        Task<GetPartnerPromotionForEditOutput> GetPartnerPromotionForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPartnerPromotionDto input);

        Task Delete(EntityDto<long> input);

        Task<List<PartnerPromotionPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task RemoveAttachmentFile(EntityDto<long> input);

    }
}