using System.Collections.Generic; 
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos; 

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners
{
    public class CreateOrEditPartnerPromotionsModalViewModel
    {
        public CreateOrEditPartnerPromotionDto PartnerPromotion { get; set; }
     
       public List<long> ProductIdList { get; set; }
        public List<PromotionProductProductLookupTableDto> PromotionProductProductList { get; set; }
        public bool IsEditMode => PartnerPromotion.Id.HasValue;

        public int PartnerId { get; set; } 
    }
}
