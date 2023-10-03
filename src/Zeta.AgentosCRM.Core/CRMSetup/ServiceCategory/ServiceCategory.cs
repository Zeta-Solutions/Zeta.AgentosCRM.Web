using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory
{
    [Table("ServiceCategories")]
    public class ServiceCategory : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ServiceCategoryConsts.MaxAbbrivationLength, MinimumLength = ServiceCategoryConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(ServiceCategoryConsts.MaxNameLength, MinimumLength = ServiceCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}