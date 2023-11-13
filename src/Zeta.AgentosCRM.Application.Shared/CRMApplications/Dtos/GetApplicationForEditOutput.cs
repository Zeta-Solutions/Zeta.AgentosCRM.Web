using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class GetApplicationForEditOutput
    {
        public CreateOrEditApplicationDto Application { get; set; }

        public string ClientFirstName { get; set; }

        public string WorkflowName { get; set; }

        public string PartnerPartnerName { get; set; }

        public string ProductName { get; set; }

    }
}