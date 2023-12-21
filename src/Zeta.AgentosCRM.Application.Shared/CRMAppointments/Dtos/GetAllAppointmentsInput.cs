using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMAppointments.Dtos
{
    public class GetAllAppointmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public DateTime? MaxAppointmentDateFilter { get; set; }
        public DateTime? MinAppointmentDateFilter { get; set; }

        public DateTime? MaxAppointmentTimeFilter { get; set; }
        public DateTime? MinAppointmentTimeFilter { get; set; }

        public string TimeZoneFilter { get; set; }

        public string ClientDisplayPropertyFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string UserNameFilter { get; set; }
        public long? ClientIdFilter { get; set; }
        public long? PartnerIdFilter { get; set; }

    }
}