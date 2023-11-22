using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAgent.Contacts.Dtos
{
    public class CreateOrEditAgentContactDto : EntityDto<long?>
    {

        [Required]
        [StringLength(AgentContactConsts.MaxNameLength, MinimumLength = AgentContactConsts.MinNameLength)]
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        [Required]
        [StringLength(AgentContactConsts.MaxEmailLength, MinimumLength = AgentContactConsts.MinEmailLength)]
        public string Email { get; set; }

        public bool IsPrimary { get; set; }

        public long AgentId { get; set; }

    }
}