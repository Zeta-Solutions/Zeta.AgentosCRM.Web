using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetAllFollowersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public string UserNameFilter { get; set; }

        public int? ClientIdFilter { get; set; }

    }
}