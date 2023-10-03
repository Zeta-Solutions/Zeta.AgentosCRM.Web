using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource
{
    [Table("LeadSources")]
    public class LeadSource : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(LeadSourceConsts.MaxAbbrivationLength, MinimumLength = LeadSourceConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(LeadSourceConsts.MaxNameLength, MinimumLength = LeadSourceConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}