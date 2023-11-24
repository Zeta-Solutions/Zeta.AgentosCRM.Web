using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class InvoiceTypeDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}