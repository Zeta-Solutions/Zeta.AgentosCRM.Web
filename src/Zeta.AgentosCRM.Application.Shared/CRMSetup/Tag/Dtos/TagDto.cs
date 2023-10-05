using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Tag.Dtos
{
    public class TagDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}