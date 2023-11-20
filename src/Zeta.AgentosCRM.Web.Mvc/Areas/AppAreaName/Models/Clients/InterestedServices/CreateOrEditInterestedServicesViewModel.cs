using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.InterestedServices
{
    public class CreateOrEditInterestedServicesViewModel
    {
        public CreateOrEditClientInterstedServiceDto ClientInterestedService { get; set; }
        public List<ClientInterstedServiceClientLookupTableDto> ClientInterestedServiceList { get; set; }
        public List<ClientInterstedServicePartnerLookupTableDto> ClientInterestedServicePartnerList { get; set; }
        public List<ClientInterstedServiceProductLookupTableDto> ClientInterestedServiceProductList { get; set; }
        public List<ClientInterstedServiceBranchLookupTableDto> ClientInterestedServiceBranchList { get; set; }
        public List<ClientInterstedServiceWorkflowLookupTableDto> ClientInterestedServiceWorkflowList { get; set; }


        public bool IsEditMode => ClientInterestedService.Id.HasValue;
    }
}
