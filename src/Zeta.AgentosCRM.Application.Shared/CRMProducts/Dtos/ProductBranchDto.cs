using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class ProductBranchDto : EntityDto
    {
        public string Name { get; set; }

        public long ProductId { get; set; }

        public long? BranchId { get; set; }

    }
}