using System.Collections.Generic;
using Zeta.AgentosCRM.DynamicEntityProperties.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DynamicEntityProperty
{
    public class CreateEntityDynamicPropertyViewModel
    {
        public string EntityFullName { get; set; }

        public List<string> AllEntities { get; set; }

        public List<DynamicPropertyDto> DynamicProperties { get; set; }
    }
}
