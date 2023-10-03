using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.ProductType.Dtos
{
    public class GetAllProductTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AbbrivaionFilter { get; set; }

        public string NameFilter { get; set; }

        public string MasterCategoryNameFilter { get; set; }

    }
}