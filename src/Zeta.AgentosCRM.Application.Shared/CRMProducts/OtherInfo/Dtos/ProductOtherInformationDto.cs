using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos
{
    public class ProductOtherInformationDto : EntityDto
    {
        public string Name { get; set; }

        public int SubjectAreaId { get; set; }

        public int SubjectId { get; set; }

        public int DegreeLevelId { get; set; }

        public long ProductId { get; set; }

    }
}