using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Qoutation.Dtos
{
    public class GetClientQuotationDetailForEditOutput
    {
        public CreateOrEditClientQuotationDetailDto ClientQuotationDetail { get; set; }

        public string WorkflowName { get; set; }

        public string PartnerPartnerName { get; set; }

        public string BranchName { get; set; }

        public string ProductName { get; set; }

        public string ClientQuotationHeadClientName { get; set; }

    }
}