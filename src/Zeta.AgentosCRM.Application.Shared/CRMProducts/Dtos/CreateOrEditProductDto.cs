using Zeta.AgentosCRM.CRMProducts;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class CreateOrEditProductDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ProductConsts.MaxNameLength, MinimumLength = ProductConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxDescriptionLength, MinimumLength = ProductConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public string Note { get; set; }

        public bool RevenueType { get; set; }

        public Months IntakeMonth { get; set; }

        public long PartnerId { get; set; }

        public int PartnerTypeId { get; set; }

        public long BranchId { get; set; }

        public List<CreateOrEditProductBranchDto> Branches { get; set; }

    }
}