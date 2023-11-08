using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos
{
    public class GetAllBranchesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string EmailFilter { get; set; }

        public string CityFilter { get; set; }

        public string StateFilter { get; set; }

        public string StreetFilter { get; set; }

        public string ZipCodeFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string PhoneCodeFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

    }
}