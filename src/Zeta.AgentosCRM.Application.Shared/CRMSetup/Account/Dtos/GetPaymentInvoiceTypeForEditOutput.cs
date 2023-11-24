using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetPaymentInvoiceTypeForEditOutput
    {
        public CreateOrEditPaymentInvoiceTypeDto PaymentInvoiceType { get; set; }

        public string InvoiceTypeName { get; set; }

        public string ManualPaymentDetailName { get; set; }

    }
}