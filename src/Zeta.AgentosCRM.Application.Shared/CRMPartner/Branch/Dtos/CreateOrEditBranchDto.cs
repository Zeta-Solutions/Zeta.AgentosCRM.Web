using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos
{
    public class CreateOrEditBranchDto : EntityDto<long?>
    {

        [Required]
        [StringLength(BranchConsts.MaxNameLength, MinimumLength = BranchConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(BranchConsts.MaxEmailLength, MinimumLength = BranchConsts.MinEmailLength)]
        public string Email { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        public int CountryId { get; set; }

        public int PartnerId { get; set; }

    }
}