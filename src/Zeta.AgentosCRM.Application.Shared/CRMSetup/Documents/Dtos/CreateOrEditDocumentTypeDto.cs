using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class CreateOrEditDocumentTypeDto : EntityDto<int?>
    {

        [StringLength(DocumentTypeConsts.MaxAbbrivationLength, MinimumLength = DocumentTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(DocumentTypeConsts.MaxNameLength, MinimumLength = DocumentTypeConsts.MinNameLength)]
        public string Name { get; set; }

    }
}