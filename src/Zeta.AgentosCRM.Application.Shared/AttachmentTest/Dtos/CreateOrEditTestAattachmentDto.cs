using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.AttachmentTest.Dtos
{
    public class CreateOrEditTestAattachmentDto : EntityDto<int?>
    {

        [Required]
        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentToken { get; set; }

    }
}