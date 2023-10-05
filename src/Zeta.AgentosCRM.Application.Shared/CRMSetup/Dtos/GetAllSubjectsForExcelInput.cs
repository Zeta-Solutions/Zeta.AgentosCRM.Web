using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetAllSubjectsForExcelInput
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

        public string SubjectAreaNameFilter { get; set; }

    }
}