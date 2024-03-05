using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing
{
    public class GetAllInvIncomeSharingInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
