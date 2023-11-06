using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner
{
    [Table("Partners")]
    [Audited]
    public class Partner : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(PartnerConsts.MaxPartnerNameLength, MinimumLength = PartnerConsts.MinPartnerNameLength)]
        public virtual string PartnerName { get; set; }

        public virtual string Street { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string PhoneNo { get; set; }

        [Required]
        [StringLength(PartnerConsts.MaxEmailLength, MinimumLength = PartnerConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Website { get; set; }

        public virtual string University { get; set; }

        public virtual string MarketingEmail { get; set; }

        public virtual string BusinessRegNo { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual Guid ProfilePictureId { get; set; }

        [ForeignKey("ProfilePictureId")]
        public BinaryObject ProfilePictureFk { get; set; }

        public virtual int MasterCategoryId { get; set; }

        [ForeignKey("MasterCategoryId")]
        public MasterCategory MasterCategoryFk { get; set; }

        public virtual int? PartnerTypeId { get; set; }

        [ForeignKey("PartnerTypeId")]
        public PartnerType PartnerTypeFk { get; set; }

        public virtual int WorkflowId { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow WorkflowFk { get; set; }

        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual int? CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public CRMCurrency CurrencyFk { get; set; }

    }
}