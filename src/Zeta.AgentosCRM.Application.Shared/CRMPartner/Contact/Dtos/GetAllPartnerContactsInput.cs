using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.Contact.Dtos
{
    public class GetAllPartnerContactsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string EmailFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string PhoneCodeFilter { get; set; }

        public string FaxFilter { get; set; }

        public string DepartmentFilter { get; set; }

        public string PositionFilter { get; set; }

        public int? PrimaryContactFilter { get; set; }

        public string BranchNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

    }
}