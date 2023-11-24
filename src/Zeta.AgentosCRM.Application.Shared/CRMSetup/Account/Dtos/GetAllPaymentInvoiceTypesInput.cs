using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllPaymentInvoiceTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string InvoiceTypeNameFilter { get; set; }

        public string ManualPaymentDetailNameFilter { get; set; }

    }
}