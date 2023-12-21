﻿using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class NoteDto : EntityDto<long>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long? AgentId { get; set; }
        public DateTime? CreationTime { get; set; }

       public long? CreatorUserId { get; set; }
    }
}