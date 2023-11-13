using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class CreateOrEditCRMTaskDto : EntityDto<long?>
    {

        [Required]
        [StringLength(CRMTaskConsts.MaxTitleLength, MinimumLength = CRMTaskConsts.MinTitleLength)]
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime DueTime { get; set; }

        [Required]
        [StringLength(CRMTaskConsts.MaxDescriptionLength, MinimumLength = CRMTaskConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentToken { get; set; }

        public int RelatedTo { get; set; }

        public int InternalId { get; set; }

        public int TaskCategoryId { get; set; }

        public long AssigneeId { get; set; }

        public int TaskPriorityId { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long? ApplicationId { get; set; }

        public long? ApplicationStageId { get; set; }

    }
}