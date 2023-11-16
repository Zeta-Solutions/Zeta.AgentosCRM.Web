using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class GetAllProductsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DurationFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string NoteFilter { get; set; }

        public int? RevenueTypeFilter { get; set; }

        public int? IntakeMonthFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string PartnerTypeNameFilter { get; set; }

        public string BranchNameFilter { get; set; }
        public int? PartnerIdFilter { get; set; }

    }
}