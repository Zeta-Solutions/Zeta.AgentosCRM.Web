using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos
{
    public class GetLeadSourceForEditOutput
    {
        public CreateOrEditLeadSourceDto LeadSource { get; set; }

    }
}