using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos
{
    public class GetAllClientInterstedServicesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public DateTime? MaxStartDateFilter { get; set; }
        public DateTime? MinStartDateFilter { get; set; }

        public DateTime? MaxEndDateFilter { get; set; }
        public DateTime? MinEndDateFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

        public string BranchNameFilter { get; set; }

    }
}