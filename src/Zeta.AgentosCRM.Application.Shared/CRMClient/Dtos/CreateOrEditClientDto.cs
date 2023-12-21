using Zeta.AgentosCRM.CRMClient;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class CreateOrEditClientDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ClientConsts.MaxFirstNameLength, MinimumLength = ClientConsts.MinFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxLastNameLength, MinimumLength = ClientConsts.MinLastNameLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxEmailLength, MinimumLength = ClientConsts.MinEmailLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(ClientConsts.MaxPhoneNoLength, MinimumLength = ClientConsts.MinPhoneNoLength)]
        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        public DateTime DateofBirth { get; set; }

        public ContactPrefernce ContactPreferences { get; set; }

        public string University { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime PreferedIntake { get; set; }

        public string PassportNo { get; set; }

        public string VisaType { get; set; }

        public DateTime VisaExpiryDate { get; set; }

        public string AddedFrom { get; set; }

        public int Rating { get; set; }

        public int InternalId { get; set; }

        public bool ClientPortal { get; set; }

        public bool TrainingRequired { get; set; }

        public string SecondaryEmail { get; set; }

        public bool Archived { get; set; }

        public int CountryId { get; set; }

        public long AssigneeId { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public int? HighestQualificationId { get; set; }

        public int? StudyAreaId { get; set; }

        public int LeadSourceId { get; set; }

        public int? PassportCountryId { get; set; }

        public long? AgentId { get; set; }

    }
}