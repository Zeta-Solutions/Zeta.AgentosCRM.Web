using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class CreateOrEditClientQuotationHeadDto : EntityDto<long?>
    {

        public DateTime DueDate { get; set; }

        public string ClientEmail { get; set; }

        public string ClientName { get; set; }

        public long ClientId { get; set; }

        public int CurrencyId { get; set; }

        public List<CreateOrEditClientQuotationDetailDto> QuotationDetails { get; set; }

    }
}