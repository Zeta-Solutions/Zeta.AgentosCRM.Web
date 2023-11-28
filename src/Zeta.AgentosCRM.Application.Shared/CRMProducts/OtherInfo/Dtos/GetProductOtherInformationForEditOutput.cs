using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos
{
    public class GetProductOtherInformationForEditOutput
    {
        public CreateOrEditProductOtherInformationDto ProductOtherInformation { get; set; }

        public string SubjectAreaName { get; set; }

        public string SubjectName { get; set; }

        public string DegreeLevelName { get; set; }

        public string ProductName { get; set; }

    }
}