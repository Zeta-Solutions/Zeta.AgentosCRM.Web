using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.Dtos
{
    public class GetAllPartnersForExcelInput
    {
        public string Filter { get; set; }

        public string PartnerNameFilter { get; set; }

        public string StreetFilter { get; set; }

        public string CityFilter { get; set; }

        public string StateFilter { get; set; }

        public string ZipCodeFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string EmailFilter { get; set; }

        public string FaxFilter { get; set; }

        public string WebsiteFilter { get; set; }

        public string UniversityFilter { get; set; }

        public string MarketingEmailFilter { get; set; }

        public string BinaryObjectDescriptionFilter { get; set; }

        public string MasterCategoryNameFilter { get; set; }

        public string PartnerTypeNameFilter { get; set; }

        public string WorkflowNameFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string CountryDisplayProperty2Filter { get; set; }

    }
}