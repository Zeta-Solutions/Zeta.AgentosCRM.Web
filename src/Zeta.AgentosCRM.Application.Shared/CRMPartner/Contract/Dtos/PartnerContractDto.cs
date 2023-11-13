using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Contract.Dtos
{
    public class PartnerContractDto : EntityDto
    {
        public DateTime ContractExpiryDate { get; set; }

        public decimal CommissionPer { get; set; }

        public long AgentId { get; set; }

        public int RegionId { get; set; }

        public long PartnerId { get; set; }

    }
}