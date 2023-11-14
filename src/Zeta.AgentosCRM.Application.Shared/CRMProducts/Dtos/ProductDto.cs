using Zeta.AgentosCRM.CRMProducts;

using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class ProductDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool RevenueType { get; set; }

        public Months IntakeMonth { get; set; }

        public long PartnerId { get; set; }

        public int PartnerTypeId { get; set; }

        public long BranchId { get; set; }

    }
}