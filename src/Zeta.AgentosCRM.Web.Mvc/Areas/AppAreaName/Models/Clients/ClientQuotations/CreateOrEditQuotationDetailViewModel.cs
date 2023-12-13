using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.ClientQuotations
{
    public class CreateOrEditQuotationDetailViewModel
    {
        public CreateOrEditClientQuotationDetailDto ClientQuotationDetail{ get; set; }

        public string WorkflowName { get; set; }
        public string PartnerName { get; set; }
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public List<ClientQuotationDetailWorkflowLookupTableDto> ClientQuotationDetailWorkflowList { get; set; }
        public List<ClientQuotationDetailPartnerLookupTableDto> ClientQuotationDetailPartnerList { get; set; }
        public List<ClientQuotationDetailBranchLookupTableDto> ClientQuotationDetailBranchList { get; set; }
        public List<ClientQuotationDetailProductLookupTableDto> ClientQuotationDetailProducList { get; set; }
        public List<ClientQuotationDetailClientQuotationHeadLookupTableDto> ClientQuotationDetailClientQuotationHeadList { get; set; }
        public bool IsEditMode => ClientQuotationDetail.Id.HasValue;
    }
}
