using Zeta.AgentosCRM.CRMPartner.PartnerBranch; 
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner.Contact
{
    [Table("PartnerContacts")]
    [Audited]
    public class PartnerContact : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(PartnerContactConsts.MaxNameLength, MinimumLength = PartnerContactConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(PartnerContactConsts.MaxEmailLength, MinimumLength = PartnerContactConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Department { get; set; }

        public virtual string Position { get; set; }

        public virtual bool PrimaryContact { get; set; }

        public virtual long BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch BranchFk { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

    }
}