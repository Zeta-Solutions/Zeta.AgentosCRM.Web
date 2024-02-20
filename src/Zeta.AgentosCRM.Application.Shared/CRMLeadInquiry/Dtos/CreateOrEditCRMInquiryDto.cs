using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Dtos
{
    public class CreateOrEditCRMInquiryDto : EntityDto<long?>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateofBirth { get; set; }

        public string PhoneCode { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string SecondaryEmail { get; set; }

        public string ContactPreference { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string VisaType { get; set; }

        public DateTime? VisaExpiryDate { get; set; }

        public string PreferedInTake { get; set; }

        public string DegreeTitle { get; set; }

        public string Institution { get; set; }

        public DateTime? CourseStartDate { get; set; }

        public DateTime? CourseEndDate { get; set; }

        public decimal? AcademicScore { get; set; }

        public bool IsGpa { get; set; }

        public decimal? Toefl { get; set; }

        public decimal? Ielts { get; set; }

        public decimal? Pte { get; set; }

        public decimal? Sat1 { get; set; }

        public decimal? Sat2 { get; set; }

        public decimal? Gre { get; set; }

        public decimal? GMat { get; set; }

        public Guid? DocumentId { get; set; }

        public string DocumentIdToken { get; set; }

        public Guid? PictureId { get; set; }

        public string PictureIdToken { get; set; }

        public string Comments { get; set; }

        public int? Status { get; set; }

        public bool IsArchived { get; set; }

        public int? CountryId { get; set; }

        public int? PassportCountryId { get; set; }

        public int? DegreeLevelId { get; set; }

        public int? SubjectId { get; set; }

        public int? SubjectAreaId { get; set; }

        public long? OrganizationUnitId { get; set; }

        public int? LeadSourceId { get; set; }

        public int? TagId { get; set; }

    }
}