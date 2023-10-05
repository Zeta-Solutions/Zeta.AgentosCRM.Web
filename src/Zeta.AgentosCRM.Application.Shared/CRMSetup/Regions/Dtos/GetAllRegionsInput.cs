using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Regions.Dtos
{
    public class GetAllRegionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

    }
}