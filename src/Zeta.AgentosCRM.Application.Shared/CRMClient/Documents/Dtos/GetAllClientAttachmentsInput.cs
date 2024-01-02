using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Documents.Dtos
{
    public class GetAllClientAttachmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

    }
}