using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup;
using Abp.Organizations;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMSetup.Tag;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMLeadInquiry
{
    [Table("CRMInquiries")]
    [Audited]
    public class CRMInquiry : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime? DateofBirth { get; set; }

        public virtual string PhoneCode { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string Email { get; set; }

        public virtual string SecondaryEmail { get; set; }

        public virtual string ContactPreference { get; set; }

        public virtual string Street { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string VisaType { get; set; }

        public virtual DateTime? VisaExpiryDate { get; set; }

        public virtual string PreferedInTake { get; set; }

        public virtual string DegreeTitle { get; set; }

        public virtual string Institution { get; set; }

        public virtual DateTime? CourseStartDate { get; set; }

        public virtual DateTime? CourseEndDate { get; set; }

        public virtual decimal? AcademicScore { get; set; }

        public virtual bool IsGpa { get; set; }

        public virtual decimal? Toefl { get; set; }

        public virtual decimal? Ielts { get; set; }

        public virtual decimal? Pte { get; set; }

        public virtual decimal? Sat1 { get; set; }

        public virtual decimal? Sat2 { get; set; }

        public virtual decimal? Gre { get; set; }

        public virtual decimal? GMat { get; set; }
        //File

        public virtual Guid? DocumentId { get; set; } //File, (BinaryObjectId)
                                                      //File

        public virtual Guid? PictureId { get; set; } //File, (BinaryObjectId)

        public virtual string Comments { get; set; }

        public virtual int? Status { get; set; }

        public virtual bool IsArchived { get; set; }

        public virtual int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual int? PassportCountryId { get; set; }

        [ForeignKey("PassportCountryId")]
        public Country PassportCountryFk { get; set; }

        public virtual int? DegreeLevelId { get; set; }

        [ForeignKey("DegreeLevelId")]
        public DegreeLevel DegreeLevelFk { get; set; }

        public virtual int? SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject SubjectFk { get; set; }

        public virtual int? SubjectAreaId { get; set; }

        [ForeignKey("SubjectAreaId")]
        public SubjectArea SubjectAreaFk { get; set; }

        public virtual long? OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }

        public virtual int? LeadSourceId { get; set; }

        [ForeignKey("LeadSourceId")]
        public LeadSource LeadSourceFk { get; set; }

        public virtual int? TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag TagFk { get; set; }

    }
}