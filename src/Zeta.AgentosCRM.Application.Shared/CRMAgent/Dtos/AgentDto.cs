using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAgent.Dtos
{
    public class AgentDto : EntityDto<long>
    {
        public string Name { get; set; }

        public bool IsSuperAgent { get; set; }

        public bool IsSubAgent { get; set; }

        public bool IsBusiness { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public decimal IncomeSharingPer { get; set; }

        public decimal Tax { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public string ProfilePictureIdFileName { get; set; }

        public string PrimaryContactName { get; set; }

        public string TaxNo { get; set; }

        public DateTime ContractExpiryDate { get; set; }

        public decimal ClaimRevenuePer { get; set; }

        public int CountryId { get; set; }
        public int clientCount { get; set; }
        public int ApplicationCount { get; set; }

        public long? OrganizationUnitId { get; set; }

    }
}