﻿using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}