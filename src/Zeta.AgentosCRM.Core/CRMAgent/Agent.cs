using Zeta.AgentosCRM.CRMSetup.Countries;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMAgent
{
    [Table("Agents")]
    [Audited]
    public class Agent : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(AgentConsts.MaxNameLength, MinimumLength = AgentConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual bool IsSuperAgent { get; set; }

        public virtual bool IsSubAgent { get; set; }

        public virtual bool IsBusiness { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string PhoneCode { get; set; }

        [Required]
        [StringLength(AgentConsts.MaxEmailLength, MinimumLength = AgentConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual string City { get; set; }

        public virtual string Street { get; set; }

        public virtual string State { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual decimal IncomeSharingPer { get; set; }

        public virtual decimal Tax { get; set; }
        //File

        public virtual Guid? ProfilePictureId { get; set; } //File, (BinaryObjectId)

        public virtual string PrimaryContactName { get; set; }

        public virtual string TaxNo { get; set; }

        public virtual DateTime ContractExpiryDate { get; set; }

        public virtual decimal ClaimRevenuePer { get; set; }

        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual long? OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }

    }
}