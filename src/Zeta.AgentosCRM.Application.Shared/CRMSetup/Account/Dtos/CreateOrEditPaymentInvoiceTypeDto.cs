using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditPaymentInvoiceTypeDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public int? InvoiceTypeId { get; set; }

        public int? ManualPaymentDetailId { get; set; }
    }
}