using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class GetAllProductBranchesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ProductNameFilter { get; set; }

        public string BranchNameFilter { get; set; }
        public long? BranchIdFilter { get; set; }
        public long? ProductIdFilter { get; set; }

    }
}