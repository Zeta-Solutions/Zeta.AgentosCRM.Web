using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Email.Dtos
{
    public class GetAllEmailTemplatesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string EmailSubjectFilter { get; set; }

        public string EmailBodyFilter { get; set; }

    }
}