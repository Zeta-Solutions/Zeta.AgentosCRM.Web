using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMLead
{
    [Table("LeadDetails")]
    [Audited]
    public class LeadDetail : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string PropertyName { get; set; }
        [Required]
        [StringLength(LeadDetailsConsts.MaxInputtypeLength, MinimumLength = LeadDetailsConsts.MinInputtypeLength)]
        public virtual string Inputtype { get; set; }
        public virtual string Status { get; set; }
        public virtual string SectionName { get; set; }

        public virtual long LeadHeadId { get; set; }
        [ForeignKey("LeadHeadId")]
        public LeadHead LeadHeadFK { get; set; }
    }
}
