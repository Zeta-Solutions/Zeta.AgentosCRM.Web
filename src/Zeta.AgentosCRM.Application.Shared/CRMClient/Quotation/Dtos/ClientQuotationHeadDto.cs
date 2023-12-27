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

        public int ProductCount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }

    }
}