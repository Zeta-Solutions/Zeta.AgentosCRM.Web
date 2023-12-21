using Zeta.AgentosCRM.CRMClient;

using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class ClientDto : EntityDto<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

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

        public int Rating { get; set; }

        public bool ClientPortal { get; set; }

        public bool TrainingRequired { get; set; }

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