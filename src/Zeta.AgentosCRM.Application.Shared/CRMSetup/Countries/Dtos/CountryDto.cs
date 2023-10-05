using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Countries.Dtos
{
    public class CountryDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Code { get; set; }

        public int RegionId { get; set; }

    }
}