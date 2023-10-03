using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos
{
    public class ServiceCategoryDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}