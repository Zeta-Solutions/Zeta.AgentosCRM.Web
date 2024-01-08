using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos
{
    public class GetAllEmailConfigurationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string SenderEmailFilter { get; set; }

        public string SmtpServerFilter { get; set; }

        public int? MaxSmtpPortFilter { get; set; }
        public int? MinSmtpPortFilter { get; set; }

        public string SenderPasswordFilter { get; set; }

        public string UserNameFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public int? IsEnableSslFilter { get; set; }

        public string ProtocolFilter { get; set; }

    }
}