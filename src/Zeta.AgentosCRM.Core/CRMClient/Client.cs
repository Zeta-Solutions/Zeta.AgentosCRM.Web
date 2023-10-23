using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup; 
using Zeta.AgentosCRM.CRMSetup.LeadSource; 
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing; 
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient
{
    [Table("Clients")]
    [Audited]
    public class Client : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxFirstNameLength, MinimumLength = ClientConsts.MinFirstNameLength)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxLastNameLength, MinimumLength = ClientConsts.MinLastNameLength)]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxEmailLength, MinimumLength = ClientConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxPhoneNoLength, MinimumLength = ClientConsts.MinPhoneNoLength)]
        public virtual string PhoneNo { get; set; }

        public virtual DateTime DateofBirth { get; set; }

        public virtual ContactPrefernce ContactPreferences { get; set; }

        public virtual string University { get; set; }

        public virtual string Street { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual DateTime PreferedIntake { get; set; }

        public virtual string PassportNo { get; set; }

        public virtual string VisaType { get; set; }

        public virtual DateTime VisaExpiryDate { get; set; }

        public virtual string AddedFrom { get; set; }

        public virtual int Rating { get; set; }

        public virtual int InternalId { get; set; }

        public virtual bool ClientPortal { get; set; }

        public virtual bool PTETraining { get; set; }

        public virtual string SecondaryEmail { get; set; }

        public virtual ClientStatus ClientStatus { get; set; } 

        public virtual bool Archived { get; set; }

        public virtual int CountryCodeId { get; set; }

        [ForeignKey("CountryCodeId")]
        public Country CountryCodeFk { get; set; }

        public virtual long AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public User AssigneeFk { get; set; }

        public virtual Guid? ProfilePictureId { get; set; }

        [ForeignKey("ProfilePictureId")]
        public BinaryObject ProfilePictureFk { get; set; }

        public virtual int? HighestQualificationId { get; set; }

        [ForeignKey("HighestQualificationId")]
        public DegreeLevel HighestQualificationFk { get; set; }

        public virtual int? StudyAreaId { get; set; }

        [ForeignKey("StudyAreaId")]
        public SubjectArea StudyAreaFk { get; set; }

        public virtual int LeadSourceId { get; set; }

        [ForeignKey("LeadSourceId")]
        public LeadSource LeadSourceFk { get; set; }

        public virtual int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual int? PassportCountryId { get; set; }

        [ForeignKey("PassportCountryId")]
        public Country PassportCountryFk { get; set; }

    }
}