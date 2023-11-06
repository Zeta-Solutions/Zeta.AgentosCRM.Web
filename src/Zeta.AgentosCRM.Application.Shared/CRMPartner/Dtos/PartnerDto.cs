using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Dtos
{
    public class PartnerDto : EntityDto<long>
    {
        public string PartnerName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public string University { get; set; }

        public string MarketingEmail { get; set; }

        public string BusinessRegNo { get; set; }

        public string PhoneCode { get; set; }

        public Guid ProfilePictureId { get; set; }

        public int MasterCategoryId { get; set; }

        public int? PartnerTypeId { get; set; }

        public int WorkflowId { get; set; }

        public int CountryId { get; set; }

        public int? CurrencyId { get; set; }

    }
}