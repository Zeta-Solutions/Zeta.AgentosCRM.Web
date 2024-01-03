using Abp.Application.Services.Dto;
using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.Auditing.Dto
{
    public class CutomizedEntityChangeAndUserDto : EntityDto<long>
    {
        public long? UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ChangeTime { get; set; }

        public string EntityTypeFullName { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public string ChangeTypeName => ChangeType.ToString();

        public long EntityChangeSetId { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public string ChangedEntities { get; set; }
    }
}
