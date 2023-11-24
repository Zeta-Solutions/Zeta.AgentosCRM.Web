using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditInvoiceTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(InvoiceTypeConsts.MaxAbbrivationLength, MinimumLength = InvoiceTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(InvoiceTypeConsts.MaxNameLength, MinimumLength = InvoiceTypeConsts.MinNameLength)]
        public string Name { get; set; }

    }
}