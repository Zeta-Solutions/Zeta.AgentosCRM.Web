using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Dtos
{
    public class GetPartnerForEditOutput
    {
        public CreateOrEditPartnerDto Partner { get; set; }

        public string BinaryObjectDescription { get; set; }

        public string MasterCategoryName { get; set; }

        public string PartnerTypeName { get; set; }

        public string WorkflowName { get; set; }

        public string CountryName { get; set; }

        public string CountryDisplayProperty2 { get; set; }

    }
}