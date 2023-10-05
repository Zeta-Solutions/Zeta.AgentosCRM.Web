using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DegreeLevel
{
    public class CreateOrEditDegreeLevelModalViewModel
    {
        public CreateOrEditDegreeLevelDto DegreeLevel { get; set; }
        public bool IsEditMode => DegreeLevel.Id.HasValue;
    }
}
