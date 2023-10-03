using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos
{
    public class CreateOrEditLeadSourceDto : EntityDto<int?>
    {

        [Required]
        [StringLength(LeadSourceConsts.MaxAbbrivationLength, MinimumLength = LeadSourceConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(LeadSourceConsts.MaxNameLength, MinimumLength = LeadSourceConsts.MinNameLength)]
        public string Name { get; set; }

    }
}