using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditPaymentInvoiceTypeDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public int? InvoiceTypeId { get; set; }

        public int? ManualPaymentDetailId { get; set; }

    }
}