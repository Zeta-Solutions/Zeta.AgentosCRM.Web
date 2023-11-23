using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class InvoiceAddressDto : EntityDto
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int CountryId { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}