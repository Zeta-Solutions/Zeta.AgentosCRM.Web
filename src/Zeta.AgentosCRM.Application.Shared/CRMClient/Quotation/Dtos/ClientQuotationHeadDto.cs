using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class ClientQuotationHeadDto : EntityDto<long>
    {
        public DateTime DueDate { get; set; }

        public string ClientEmail { get; set; }

        public string ClientName { get; set; }

        public long ClientId { get; set; }

        public int CurrencyId { get; set; }

    }
}