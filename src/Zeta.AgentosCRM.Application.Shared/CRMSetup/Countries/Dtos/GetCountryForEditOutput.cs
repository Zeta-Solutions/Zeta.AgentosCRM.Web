using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Countries.Dtos
{
    public class GetCountryForEditOutput
    {
        public CreateOrEditCountryDto Country { get; set; }

        public string RegionName { get; set; }

    }
}