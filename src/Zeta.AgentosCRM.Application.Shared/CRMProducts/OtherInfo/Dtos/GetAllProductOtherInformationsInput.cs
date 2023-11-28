using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos
{
    public class GetAllProductOtherInformationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string SubjectAreaNameFilter { get; set; }

        public string SubjectNameFilter { get; set; }

        public string DegreeLevelNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}