using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class GetNoteForEditOutput
    {
        public CreateOrEditNoteDto Note { get; set; }

        public string ClientDisplayProperty { get; set; }

        public string PartnerPartnerName { get; set; }

        public string CRMAgentAgentName { get; set; }

    }
}