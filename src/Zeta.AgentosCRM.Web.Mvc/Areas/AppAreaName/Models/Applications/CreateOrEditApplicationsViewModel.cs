using System.Collections.Generic;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.CRMAppointments.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Applications
{
    public class CreateOrEditApplicationsViewModel
    {

        public CreateOrEditApplicationDto Application { get; set; }
        public List<ApplicationWorkflowLookupTableDto> ApplicationWorkflowList { get; set; }
        public List<ApplicationPartnerLookupTableDto> ApplicationPartnerList { get; set; }
        public List<ApplicationProductLookupTableDto> ApplicationProductList { get; set; }

        public bool IsEditMode => Application.Id.HasValue;
    }
}
