using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class DocumentTypeDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}