using Zeta.AgentosCRM.CRMSetup.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerTypes
{
    public class CreateOrEditPartnerTypeModalViewModel
    {
        public CreateOrEditPartnerTypeDto PartnerType { get; set; }

        public string MasterCategoryName { get; set; }

        public List<PartnerTypeMasterCategoryLookupTableDto> PartnerTypeMasterCategoryList { get; set; }

        public bool IsEditMode => PartnerType.Id.HasValue;
    }
}