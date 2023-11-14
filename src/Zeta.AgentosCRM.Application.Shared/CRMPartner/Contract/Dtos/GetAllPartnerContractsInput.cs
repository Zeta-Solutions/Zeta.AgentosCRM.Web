using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.Contract.Dtos
{
    public class GetAllPartnerContractsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public DateTime? MaxContractExpiryDateFilter { get; set; }
        public DateTime? MinContractExpiryDateFilter { get; set; }

        public decimal? MaxCommissionPerFilter { get; set; }
        public decimal? MinCommissionPerFilter { get; set; }

        public string AgentNameFilter { get; set; }

        public string RegionNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

    }
}