using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllInvoiceAddressesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string StreetFilter { get; set; }

        public string CityFilter { get; set; }

        public string StateFilter { get; set; }

        public string ZipCodeFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }
        public int? OrganizationUnitIdFilter { get; set; }

    }
}