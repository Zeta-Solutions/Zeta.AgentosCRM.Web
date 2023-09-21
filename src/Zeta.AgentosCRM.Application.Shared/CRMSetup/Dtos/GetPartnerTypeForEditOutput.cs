using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetPartnerTypeForEditOutput
    {
        public CreateOrEditPartnerTypeDto PartnerType { get; set; }

        public string MasterCategoryName { get; set; }

    }
}