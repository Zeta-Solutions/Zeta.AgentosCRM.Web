using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Regions.Dtos
{
    public class GetRegionForEditOutput
    {
        public CreateOrEditRegionDto Region { get; set; }

    }
}