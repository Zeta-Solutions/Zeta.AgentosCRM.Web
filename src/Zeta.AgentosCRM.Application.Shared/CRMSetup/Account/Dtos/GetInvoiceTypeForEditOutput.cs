using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetInvoiceTypeForEditOutput
    {
        public CreateOrEditInvoiceTypeDto InvoiceType { get; set; }

    }
}