using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Contact.Dtos
{
    public class CreateOrEditPartnerContactDto : EntityDto<long?>
    {

        [Required]
        [StringLength(PartnerContactConsts.MaxNameLength, MinimumLength = PartnerContactConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PartnerContactConsts.MaxEmailLength, MinimumLength = PartnerContactConsts.MinEmailLength)]
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string PhoneCode { get; set; }

        public string Fax { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public bool PrimaryContact { get; set; }

        public long BranchId { get; set; }

        public long PartnerId { get; set; }

    }
}