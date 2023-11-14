using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.AttachmentTest.Dtos
{
    public class TestAattachmentDto : EntityDto
    {
        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentFileName { get; set; }

    }
}