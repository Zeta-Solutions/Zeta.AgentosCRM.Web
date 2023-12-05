using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class GetProductBranchForEditOutput
    {
        public CreateOrEditProductBranchDto ProductBranch { get; set; }

        public string ProductName { get; set; }

        public string BranchName { get; set; }

    }
}