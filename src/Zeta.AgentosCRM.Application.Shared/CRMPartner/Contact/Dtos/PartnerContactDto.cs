using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Contact.Dtos
{
    public class PartnerContactDto : EntityDto<long>
    {
        public string Name { get; set; }

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