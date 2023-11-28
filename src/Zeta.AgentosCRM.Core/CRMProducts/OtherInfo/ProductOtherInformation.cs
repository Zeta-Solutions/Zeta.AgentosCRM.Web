using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo
{
    [Table("ProductOtherInformations")]
    [Audited]
    public class ProductOtherInformation : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual int SubjectAreaId { get; set; }

        [ForeignKey("SubjectAreaId")]
        public SubjectArea SubjectAreaFk { get; set; }

        public virtual int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject SubjectFk { get; set; }

        public virtual int DegreeLevelId { get; set; }

        [ForeignKey("DegreeLevelId")]
        public DegreeLevel DegreeLevelFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

    }
}