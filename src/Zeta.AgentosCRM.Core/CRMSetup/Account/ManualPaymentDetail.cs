using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [Table("ManualPaymentDetails")]
    [Audited]
    public class ManualPaymentDetail : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ManualPaymentDetailConsts.MaxNameLength, MinimumLength = ManualPaymentDetailConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        public virtual string PaymentDetail { get; set; }

        public virtual long OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }

    }
}