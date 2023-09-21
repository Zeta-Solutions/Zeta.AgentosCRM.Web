using System.Collections.Generic;
using Zeta.AgentosCRM.Caching.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
        
        public bool CanClearAllCaches { get; set; }
    }
}