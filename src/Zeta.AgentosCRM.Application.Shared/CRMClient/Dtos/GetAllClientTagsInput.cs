using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetAllClientTagsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public string TagNameFilter { get; set; }

        public int? ClientIdFilter { get; set; }

    }
}