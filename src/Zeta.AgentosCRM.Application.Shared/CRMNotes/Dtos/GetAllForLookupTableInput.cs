﻿using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}