using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAgent.Contacts.Dtos
{
    public class GetAgentContactForEditOutput
    {
        public CreateOrEditAgentContactDto AgentContact { get; set; }

        public string AgentName { get; set; }

    }
}