using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Dtos
{
    public class GetAllCRMInquiriesForExcelInput
    {
        public string Filter { get; set; }

        public string FirstNameFilter { get; set; }

        public string LastNameFilter { get; set; }

        public DateTime? MaxDateofBirthFilter { get; set; }
        public DateTime? MinDateofBirthFilter { get; set; }

        public string PhoneCodeFilter { get; set; }

        public string PhoneNoFilter { get; set; }

        public string EmailFilter { get; set; }

        public string SecondaryEmailFilter { get; set; }

        public string ContactPreferenceFilter { get; set; }

        public string StreetFilter { get; set; }

        public string CityFilter { get; set; }

        public string StateFilter { get; set; }

        public string PostalCodeFilter { get; set; }

        public string VisaTypeFilter { get; set; }

        public DateTime? MaxVisaExpiryDateFilter { get; set; }
        public DateTime? MinVisaExpiryDateFilter { get; set; }

        public string PreferedInTakeFilter { get; set; }

        public string DegreeTitleFilter { get; set; }

        public string InstitutionFilter { get; set; }

        public DateTime? MaxCourseStartDateFilter { get; set; }
        public DateTime? MinCourseStartDateFilter { get; set; }

        public DateTime? MaxCourseEndDateFilter { get; set; }
        public DateTime? MinCourseEndDateFilter { get; set; }

        public decimal? MaxAcademicScoreFilter { get; set; }
        public decimal? MinAcademicScoreFilter { get; set; }

        public int? IsGpaFilter { get; set; }

        public decimal? MaxToeflFilter { get; set; }
        public decimal? MinToeflFilter { get; set; }

        public decimal? MaxIeltsFilter { get; set; }
        public decimal? MinIeltsFilter { get; set; }

        public decimal? MaxPteFilter { get; set; }
        public decimal? MinPteFilter { get; set; }

        public decimal? MaxSat1Filter { get; set; }
        public decimal? MinSat1Filter { get; set; }

        public decimal? MaxSat2Filter { get; set; }
        public decimal? MinSat2Filter { get; set; }

        public decimal? MaxGreFilter { get; set; }
        public decimal? MinGreFilter { get; set; }

        public decimal? MaxGMatFilter { get; set; }
        public decimal? MinGMatFilter { get; set; }

        public string CommentsFilter { get; set; }

        public int? MaxStatusFilter { get; set; }
        public int? MinStatusFilter { get; set; }

        public int? IsArchivedFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string CountryName2Filter { get; set; }

        public string DegreeLevelNameFilter { get; set; }

        public string SubjectNameFilter { get; set; }

        public string SubjectAreaNameFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

        public string LeadSourceNameFilter { get; set; }

        public string TagNameFilter { get; set; }

    }
}