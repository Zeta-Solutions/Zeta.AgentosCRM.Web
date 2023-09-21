using System.Collections.Generic;
using Zeta.AgentosCRM.DynamicEntityProperties.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
