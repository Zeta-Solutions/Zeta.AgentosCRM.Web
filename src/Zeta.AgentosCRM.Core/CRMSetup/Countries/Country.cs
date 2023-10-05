using Zeta.AgentosCRM.CRMSetup.Regions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Countries
{
    [Table("Countries")]
    public class Country : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxAbbrivationLength, MinimumLength = CountryConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [StringLength(CountryConsts.MaxIconLength, MinimumLength = CountryConsts.MinIconLength)]
        public virtual string Icon { get; set; }

        [StringLength(CountryConsts.MaxCodeLength, MinimumLength = CountryConsts.MinCodeLength)]
        public virtual string Code { get; set; }

        public virtual int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Region RegionFk { get; set; }

    }
}