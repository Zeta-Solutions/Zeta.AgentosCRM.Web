using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Contract.Dtos
{
    public class GetPartnerContractForEditOutput
    {
        public CreateOrEditPartnerContractDto PartnerContract { get; set; }

        public string AgentName { get; set; }

        public string RegionName { get; set; }

        public string PartnerPartnerName { get; set; }

    }
}