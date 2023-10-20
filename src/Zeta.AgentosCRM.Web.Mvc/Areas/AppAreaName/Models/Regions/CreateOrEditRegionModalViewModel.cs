using Zeta.AgentosCRM.CRMSetup.Regions.Dtos;
using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Regions
{
    public class CreateOrEditRegionModalViewModel
    {
        public CreateOrEditRegionDto Region { get; set; }

        public bool IsEditMode => Region.Id.HasValue;
    }
}
