using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class CreateOrEditClientQuotationHeadDto : EntityDto<long?>
    {

        public DateTime DueDate { get; set; }

        public string ClientEmail { get; set; }

        public string ClientName { get; set; }

        public long ClientId { get; set; }

        public int CurrencyId { get; set; }

    }
}