namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetPaymentInvoiceTypeForViewDto
    {
        public PaymentInvoiceTypeDto PaymentInvoiceType { get; set; }

        public string InvoiceTypeName { get; set; }

        public string ManualPaymentDetailName { get; set; }

    }
}