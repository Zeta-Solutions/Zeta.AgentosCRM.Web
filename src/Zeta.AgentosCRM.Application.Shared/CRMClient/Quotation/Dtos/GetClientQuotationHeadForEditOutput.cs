using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.CRMClient.Qoutation.Dtos;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetClientQuotationHeadForEditOutput
    {
        public CreateOrEditClientQuotationHeadDto ClientQuotationHead { get; set; }

        public string ClientFirstName { get; set; }

        public string CRMCurrencyName { get; set; }

		public List<CreateOrEditClientQuotationDetailDto> ClientQuotationDetail { get; set; }

	}
}