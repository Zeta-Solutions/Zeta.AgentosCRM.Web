using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Organizations;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMSetup.Tag;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMLead
{
    [Table("LeadHead")]
    [Audited]
    public class LeadHead : FullAuditedEntity<long>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }
        [Required]
        [StringLength(LeadConsts.MaxFormNameLength, MinimumLength = LeadConsts.MinFormNameLength)]
        public virtual string FormName { get; set; }
        public virtual string CoverImage { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Formtittle { get; set; }
        public virtual string HeaderNote { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual int? LeadSourceId { get; set; }
        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFK { get; set; }

        [ForeignKey("LeadSourceId")]
        public LeadSource LeadSourceFk { get; set; }

        public virtual string TagName  { get; set; }
        public virtual bool IsPrivacyShown  { get; set; }
        public virtual string PrivacyInfo  { get; set; }
        public virtual string Consent  { get; set; }
    }
}
