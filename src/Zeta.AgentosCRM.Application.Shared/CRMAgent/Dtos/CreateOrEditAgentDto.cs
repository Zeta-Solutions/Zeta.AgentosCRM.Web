using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAgent.Dtos
{
    public class CreateOrEditAgentDto : EntityDto<long?>
    {

        [Required]
        [StringLength(AgentConsts.MaxNameLength, MinimumLength = AgentConsts.MinNameLength)]
        public string Name { get; set; }

        public bool IsSuperAgent { get; set; }

        public bool IsBusiness { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        [Required]
        [StringLength(AgentConsts.MaxEmailLength, MinimumLength = AgentConsts.MinEmailLength)]
        public string Email { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public decimal IncomeSharingPer { get; set; }

        public decimal Tax { get; set; }

        public Guid? ProfileImageId { get; set; }

        public string ProfileImageIdToken { get; set; }

        public string PrimaryContactName { get; set; }

        public string TaxNo { get; set; }

        public DateTime ContractExpiryDate { get; set; }

        public decimal ClaimRevenuePer { get; set; }

        public int CountryId { get; set; }

        public long? OrganizationUnitId { get; set; }

    }
}