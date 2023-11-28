﻿using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.InstallmentType;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    [Table("ProductFees")]
    [Audited]
    public class ProductFee : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(ProductFeeConsts.MaxNameLength, MinimumLength = ProductFeeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int Installments { get; set; }

        public virtual decimal InstallmentAmount { get; set; }

        public virtual decimal TotalFee { get; set; }

        public virtual string ClaimTerms { get; set; }

        public virtual decimal CommissionPer { get; set; }

        public virtual bool AddInQuotation { get; set; }

        public virtual decimal NetTotal { get; set; }

        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual int? InstallmentTypeId { get; set; }

        [ForeignKey("InstallmentTypeId")]
        public InstallmentType InstallmentTypeFk { get; set; }

        public virtual int? FeeTypeId { get; set; }

        [ForeignKey("FeeTypeId")]
        public FeeType FeeTypeFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

    }
}