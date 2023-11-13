﻿using Zeta.AgentosCRM.CRMApplications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMApplications.Stages
{
    [Table("ApplicationStages")]
    [Audited]
    public class ApplicationStage : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ApplicationStageConsts.MaxNameLength, MinimumLength = ApplicationStageConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual long ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application ApplicationFk { get; set; }

    }
}