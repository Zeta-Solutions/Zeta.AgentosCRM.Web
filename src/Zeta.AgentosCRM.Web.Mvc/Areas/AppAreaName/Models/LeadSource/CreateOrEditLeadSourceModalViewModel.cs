using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource
{
    public class CreateOrEditLeadSourceModalViewModel
    {
        public CreateOrEditLeadSourceDto LeadSource { get; set; }

        public bool IsEditMode => LeadSource.Id.HasValue;
    }
}
