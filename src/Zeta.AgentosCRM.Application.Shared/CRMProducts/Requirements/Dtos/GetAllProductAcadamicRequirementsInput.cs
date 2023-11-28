using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetAllProductAcadamicRequirementsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public double? MaxAcadamicScoreFilter { get; set; }
        public double? MinAcadamicScoreFilter { get; set; }

        public int? IsGPAFilter { get; set; }

        public string DegreeLevelNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}