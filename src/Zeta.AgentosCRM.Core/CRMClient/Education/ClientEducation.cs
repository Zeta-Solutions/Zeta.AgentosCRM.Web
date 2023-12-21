using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    [Table("ClientEducations")]
    [Audited]
    public class ClientEducation : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ClientEducationConsts.MaxDegreeTitleLength, MinimumLength = ClientEducationConsts.MinDegreeTitleLength)]
        public virtual string DegreeTitle { get; set; }

        [Required]
        [StringLength(ClientEducationConsts.MaxInstitutionLength, MinimumLength = ClientEducationConsts.MinInstitutionLength)]
        public virtual string Institution { get; set; }

        public virtual DateTime CourseStartDate { get; set; }

        public virtual DateTime CourseEndDate { get; set; }

        public virtual double AcadmicScore { get; set; }

        public virtual int DegreeLevelId { get; set; }

        public virtual bool IsGpa { get; set; }

        [ForeignKey("DegreeLevelId")]
        public DegreeLevel DegreeLevelFk { get; set; }

        public virtual int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject SubjectFk { get; set; }

        public virtual int SubjectAreaId { get; set; }

        [ForeignKey("SubjectAreaId")]
        public SubjectArea SubjectAreaFk { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

    }
}