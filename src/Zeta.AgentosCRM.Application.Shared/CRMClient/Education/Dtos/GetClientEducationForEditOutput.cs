using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetClientEducationForEditOutput
    {
        public CreateOrEditClientEducationDto ClientEducation { get; set; }

        public string DegreeLevelName { get; set; }

        public string SubjectName { get; set; }

        public string SubjectAreaName { get; set; }

    }
}