using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Documents.Dtos
{
    public class CreateOrEditClientAttachmentDto : EntityDto<long?>
    {

        public string Name { get; set; }

        public Guid? AttachmentId { get; set; }

        //public string AttachmentIdToken { get; set; }

        public long? ClientId { get; set; }

    }
}