using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetAllClientsForExcelInput
    {
        public string Filter { get; set; }

        public string FirstNameFilter { get; set; }

        public string LastNameFilter { get; set; }

        public string EmailFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public DateTime? MaxDateofBirthFilter { get; set; }
        public DateTime? MinDateofBirthFilter { get; set; }

        public string UniversityFilter { get; set; }

        public int? MaxRatingFilter { get; set; }
        public int? MinRatingFilter { get; set; }

        public string CountryDisplayPropertyFilter { get; set; }

        public string UserNameFilter { get; set; }

        public string BinaryObjectDescriptionFilter { get; set; }

        public string DegreeLevelNameFilter { get; set; }

        public string SubjectAreaNameFilter { get; set; }

        public string LeadSourceNameFilter { get; set; }

        public string CountryName2Filter { get; set; }

        public string CountryName3Filter { get; set; }

    }
}