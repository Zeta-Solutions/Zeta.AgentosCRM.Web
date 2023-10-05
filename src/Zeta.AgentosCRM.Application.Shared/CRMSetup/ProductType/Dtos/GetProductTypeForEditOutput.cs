using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.ProductType.Dtos
{
    public class GetProductTypeForEditOutput
    {
        public CreateOrEditProductTypeDto ProductType { get; set; }

        public string MasterCategoryName { get; set; }

    }
}