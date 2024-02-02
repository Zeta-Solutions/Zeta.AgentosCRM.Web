using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class GetAllLeadDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
