using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetClientQuotationHeadForEditOutput
    {
        public CreateOrEditClientQuotationHeadDto ClientQuotationHead { get; set; }

        public string ClientFirstName { get; set; }

        public string CRMCurrencyName { get; set; }

    }
}