using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllTaxSettingsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TaxCodeFilter { get; set; }

        public decimal? MaxTaxRateFilter { get; set; }
        public decimal? MinTaxRateFilter { get; set; }

        public int? IsDefaultFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

        public int? OrganizationUnitIdFilter { get; set; }

    }
}