using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetEnglisTestScoreForEditOutput
    {
        public CreateOrEditEnglisTestScoreDto EnglisTestScore { get; set; }

        public string ClientFirstName { get; set; }

    }
}