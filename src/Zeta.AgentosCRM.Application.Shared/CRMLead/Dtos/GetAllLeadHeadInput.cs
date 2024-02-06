using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class GetAllLeadHeadInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string FormNameFilter { get; set; }

    }
}
