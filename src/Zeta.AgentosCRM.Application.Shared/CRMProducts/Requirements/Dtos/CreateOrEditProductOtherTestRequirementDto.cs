using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class CreateOrEditProductOtherTestRequirementDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public double TotalScore { get; set; }

        public long ProductId { get; set; }

    }
}