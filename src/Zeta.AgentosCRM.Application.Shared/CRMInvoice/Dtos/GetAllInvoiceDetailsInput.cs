using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetAllInvoiceDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
