using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Countries.Dtos
{
    public class GetAllCountriesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

        public string IconFilter { get; set; }

        public string CodeFilter { get; set; }

        public string RegionNameFilter { get; set; }

    }
}