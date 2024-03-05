using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived
{
    public class GetAllInvPaymentReceivedInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
