using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.AttachmentTest.Dtos
{
    public class GetTestAattachmentForEditOutput
    {
        public CreateOrEditTestAattachmentDto TestAattachment { get; set; }

        public string AttachmentFileName { get; set; }

    }
}