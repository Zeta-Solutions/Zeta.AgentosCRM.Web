using Zeta.AgentosCRM.CRMSetup.Countries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch
{
    [Table("Branches")]
    [Audited]
    public class Branch : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(BranchConsts.MaxNameLength, MinimumLength = BranchConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(BranchConsts.MaxEmailLength, MinimumLength = BranchConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Street { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

    }
}