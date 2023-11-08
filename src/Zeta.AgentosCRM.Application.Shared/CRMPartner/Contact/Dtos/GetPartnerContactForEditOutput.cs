using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Contact.Dtos
{
    public class GetPartnerContactForEditOutput
    {
        public CreateOrEditPartnerContactDto PartnerContact { get; set; }

        public string BranchName { get; set; }

        public string PartnerPartnerName { get; set; }

    }
}