using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMAgent.Contacts.Dtos
{
    public class GetAllAgentContactsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string PhoneCodeFilter { get; set; }

        public string EmailFilter { get; set; }

        public int? IsPrimaryFilter { get; set; }

        public string AgentNameFilter { get; set; }

    }
}