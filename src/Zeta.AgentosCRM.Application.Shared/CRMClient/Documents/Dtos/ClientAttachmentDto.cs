using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Documents.Dtos
{
    public class ClientAttachmentDto : EntityDto<long>
    {
        public string Name { get; set; }

        public Guid? AttachmentId { get; set; }

        public string AttachmentIdFileName { get; set; }

        public long? ClientId { get; set; }
        public DateTime? CreationTime { get; set; }

    }
}