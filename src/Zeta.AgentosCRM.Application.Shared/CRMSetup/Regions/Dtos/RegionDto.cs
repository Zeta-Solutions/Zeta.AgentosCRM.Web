using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Regions.Dtos
{
    public class RegionDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}