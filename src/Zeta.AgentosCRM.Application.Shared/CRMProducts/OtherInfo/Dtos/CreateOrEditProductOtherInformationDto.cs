using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos
{
    public class CreateOrEditProductOtherInformationDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public int SubjectAreaId { get; set; }

        public int SubjectId { get; set; }

        public int DegreeLevelId { get; set; }

        public long ProductId { get; set; }

    }
}