using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class GetAllSentEmailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string SubjectFilter { get; set; }

        public string FromEmailFilter { get; set; }

        public string ToEmailFilter { get; set; }

        public string CCEmailFilter { get; set; }

        public string BCCEmailFilter { get; set; }

        public string EmailBodyFilter { get; set; }

        public int? IsSentFilter { get; set; }

        public string EmailTemplateTitleFilter { get; set; }

        public string EmailConfigurationNameFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public int? ClientIdFilter { get; set; }

        public string ApplicationNameFilter { get; set; }

    }
}