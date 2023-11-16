using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos
{
    public class GetClientInterstedServiceForEditOutput
    {
        public CreateOrEditClientInterstedServiceDto ClientInterstedService { get; set; }

        public string ClientFirstName { get; set; }

        public string PartnerPartnerName { get; set; }

        public string ProductName { get; set; }

        public string BranchName { get; set; }

        public string WorkflowName { get; set; }

    }
}