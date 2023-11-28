using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetAllProductOtherTestRequirementsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public double? MaxTotalScoreFilter { get; set; }
        public double? MinTotalScoreFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}