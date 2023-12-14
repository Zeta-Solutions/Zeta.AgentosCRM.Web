using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAgent.Dtos
{
    public class GetAgentForEditOutput
    {
        public CreateOrEditAgentDto Agent { get; set; }

        public string CountryName { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

        public string ProfilePictureIdFileName { get; set; }

    }
}