using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class GetApplicationStageForEditOutput
    {
        public CreateOrEditApplicationStageDto ApplicationStage { get; set; }

        public string ApplicationName { get; set; }

    }
}