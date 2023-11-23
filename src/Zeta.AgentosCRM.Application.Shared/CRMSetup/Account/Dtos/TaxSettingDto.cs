using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class TaxSettingDto : EntityDto
    {
        public string TaxCode { get; set; }

        public decimal TaxRate { get; set; }

        public bool IsDefault { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}