using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.FeeType.Dtos
{
    public class FeeTypeDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}