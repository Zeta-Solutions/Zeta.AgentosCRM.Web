using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Documents.Dtos
{
    public class GetClientAttachmentForEditOutput
    {
        public CreateOrEditClientAttachmentDto ClientAttachment { get; set; }

        public string ClientFirstName { get; set; }

        public string AttachmentIdFileName { get; set; }

    }
}