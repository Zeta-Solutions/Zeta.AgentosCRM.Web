using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Contract.Dtos
{
    public class CreateOrEditPartnerContractDto : EntityDto<int?>
    {

        public DateTime ContractExpiryDate { get; set; }

        public decimal CommissionPer { get; set; }

        public long AgentId { get; set; }

        public int RegionId { get; set; }

        public long PartnerId { get; set; }

    }
}