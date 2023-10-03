using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos
{
    public class LeadSourceDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}