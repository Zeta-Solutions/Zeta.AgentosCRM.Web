using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos
{
    public class GetServiceCategoryForEditOutput
    {
        public CreateOrEditServiceCategoryDto ServiceCategory { get; set; }

    }
}