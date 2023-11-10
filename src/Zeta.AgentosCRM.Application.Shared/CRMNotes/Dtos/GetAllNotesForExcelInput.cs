using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class GetAllNotesForExcelInput
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string ClientDisplayPropertyFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

    }
}