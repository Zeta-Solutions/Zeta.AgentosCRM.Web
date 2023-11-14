using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos
{
    public class GetBranchForEditOutput
    {
        public CreateOrEditBranchDto Branch { get; set; }

        public string CountryName { get; set; }

        public string PartnerPartnerName { get; set; }

    }
}