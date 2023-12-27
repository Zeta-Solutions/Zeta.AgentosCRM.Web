using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class CRMTaskDto : EntityDto<long>
    {
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime DueTime { get; set; }

        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentFileName { get; set; }

        public int RelatedTo { get; set; }

        public int InternalId { get; set; }

        public int TaskCategoryId { get; set; }

        public long AssigneeId { get; set; }

        public int TaskPriorityId { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long? ApplicationId { get; set; }

        public long? ApplicationStageId { get; set; }
        public bool? IsCompleted { get; set; }

        

    }
}