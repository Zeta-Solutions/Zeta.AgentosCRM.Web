using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class CreateOrEditProductBranchDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public long ProductId { get; set; }

        public long? BranchId { get; set; }

    }
}