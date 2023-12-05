using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetManualPaymentDetailForEditOutput
    {
        public CreateOrEditManualPaymentDetailDto ManualPaymentDetail { get; set; }

        public string OrganizationUnitDisplayName { get; set; }
        public List<CreateOrEditPaymentInvoiceTypeDto> PaymentInvoiceType { get; set; }

    }
}