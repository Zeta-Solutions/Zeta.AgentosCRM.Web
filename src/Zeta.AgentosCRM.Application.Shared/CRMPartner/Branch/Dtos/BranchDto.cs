using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos
{
    public class BranchDto : EntityDto<long>
    {
        public string Name { get; set; }

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