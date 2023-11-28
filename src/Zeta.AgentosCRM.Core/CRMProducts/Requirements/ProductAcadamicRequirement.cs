using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    [Table("ProductAcadamicRequirements")]
    [Audited]
    public class ProductAcadamicRequirement : AuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Title { get; set; }

        public virtual double AcadamicScore { get; set; }

        public virtual bool IsGPA { get; set; }

        public virtual int DegreeLevelId { get; set; }

        [ForeignKey("DegreeLevelId")]
        public DegreeLevel DegreeLevelFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

    }
}