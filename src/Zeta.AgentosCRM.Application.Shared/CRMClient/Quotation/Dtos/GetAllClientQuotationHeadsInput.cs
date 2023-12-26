using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetAllClientQuotationHeadsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public DateTime? MaxDueDateFilter { get; set; }
        public DateTime? MinDueDateFilter { get; set; }

        public string ClientEmailFilter { get; set; }

        public string ClientNameFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public string CRMCurrencyNameFilter { get; set; }
        public long? ClientIdFilter { get; set; }

    }
}