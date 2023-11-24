using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllBusinessRegNummbersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string RegistrationNoFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

    }
}