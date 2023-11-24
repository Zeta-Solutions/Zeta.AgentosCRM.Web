using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAgent.Contacts.Dtos
{
    public class AgentContactDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        public string Email { get; set; }

        public bool IsPrimary { get; set; }

        public long AgentId { get; set; }

    }
}