using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos
{
    public class CRMCurrencyDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}