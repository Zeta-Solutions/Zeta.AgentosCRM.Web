using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class PaymentInvoiceTypeDto : EntityDto
    {
        public string Name { get; set; }

        public int? InvoiceTypeId { get; set; }

        public int? ManualPaymentDetailId { get; set; }

    }
}