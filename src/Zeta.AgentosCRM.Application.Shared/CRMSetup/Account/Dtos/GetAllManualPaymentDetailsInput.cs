using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllManualPaymentDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string PaymentDetailFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

        public int? OrganizationUnitIdFilter { get; set; }
    }
}