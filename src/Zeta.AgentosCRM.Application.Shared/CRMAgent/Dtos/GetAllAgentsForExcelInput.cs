using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMAgent.Dtos
{
    public class GetAllAgentsForExcelInput
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public int? IsSuperAgentFilter { get; set; }

        public int? IsBusinessFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string EmailFilter { get; set; }

        public string CityFilter { get; set; }

        public string StreetFilter { get; set; }

        public string StateFilter { get; set; }

        public decimal? MaxIncomeSharingPerFilter { get; set; }
        public decimal? MinIncomeSharingPerFilter { get; set; }

        public decimal? MaxTaxFilter { get; set; }
        public decimal? MinTaxFilter { get; set; }

        public string PrimaryContactNameFilter { get; set; }

        public string TaxNoFilter { get; set; }

        public DateTime? MaxContractExpiryDateFilter { get; set; }
        public DateTime? MinContractExpiryDateFilter { get; set; }

        public decimal? MaxClaimRevenuePerFilter { get; set; }
        public decimal? MinClaimRevenuePerFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

    }
}