using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class SubjectDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}