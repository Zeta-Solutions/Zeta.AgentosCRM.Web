using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditManualPaymentDetailDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ManualPaymentDetailConsts.MaxNameLength, MinimumLength = ManualPaymentDetailConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        public string PaymentDetail { get; set; }

        public long OrganizationUnitId { get; set; }
        public List<CreateOrEditPaymentInvoiceTypeDto> PaymentInvoiceTypeRecord { get; set; }

    }
}