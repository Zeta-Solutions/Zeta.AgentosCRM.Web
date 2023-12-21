using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class CreateOrEditNoteDto : EntityDto<long?>
    {

        [Required]
        [StringLength(NoteConsts.MaxTitleLength, MinimumLength = NoteConsts.MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(NoteConsts.MaxDescriptionLength, MinimumLength = NoteConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long? AgentId { get; set; }

        public long? ApplicationStageId { get; set; }

        public long? ApplicationId { get; set; }


    }
}