using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos
{
    public class GetCRMCurrencyForEditOutput
    {
        public CreateOrEditCRMCurrencyDto CRMCurrency { get; set; }

    }
}