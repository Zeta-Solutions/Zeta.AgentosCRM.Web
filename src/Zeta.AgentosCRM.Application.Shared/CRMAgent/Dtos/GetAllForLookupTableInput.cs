﻿using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAgent.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}