using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos
{
    public class InstallmentTypeDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}