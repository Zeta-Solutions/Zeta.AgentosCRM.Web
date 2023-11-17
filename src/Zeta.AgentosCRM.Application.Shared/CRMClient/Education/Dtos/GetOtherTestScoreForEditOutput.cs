using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetOtherTestScoreForEditOutput
    {
        public CreateOrEditOtherTestScoreDto OtherTestScore { get; set; }

        public string ClientFirstName { get; set; }

    }
}