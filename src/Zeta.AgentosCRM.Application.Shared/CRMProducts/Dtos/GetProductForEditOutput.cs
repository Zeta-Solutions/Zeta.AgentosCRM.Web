using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class GetProductForEditOutput
    {
        public CreateOrEditProductDto Product { get; set; }

        public string PartnerPartnerName { get; set; }

        public string PartnerTypeName { get; set; }

        public string BranchName { get; set; }

    }
}