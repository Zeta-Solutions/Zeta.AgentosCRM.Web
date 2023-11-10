using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.AttachmentTest.Dtos
{
    public class GetAllTestAattachmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string DescriptionFilter { get; set; }

    }
}