using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners
{
    public class CreateOrEditPartnerPromotionsModalViewModel
    {
        public CreateOrEditPartnerPromotionDto PartnerPromotion { get; set; }

        public bool IsEditMode => PartnerPromotion.Id.HasValue;

        public int PartnerId { get; set; }
    }
}
