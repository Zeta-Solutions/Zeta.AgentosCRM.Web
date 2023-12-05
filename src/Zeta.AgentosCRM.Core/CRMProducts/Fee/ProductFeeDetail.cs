using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMProducts.Fee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    [Table("ProductFeeDetails")]
    [Audited]
    public class ProductFeeDetail : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual decimal InstallmentAmount { get; set; }

        public virtual int Installments { get; set; }

        public virtual decimal TotalFee { get; set; }

        public virtual string ClaimTerms { get; set; }

        public virtual double CommissionPer { get; set; }

        public virtual bool IsPayable { get; set; }

        public virtual bool AddInQuotation { get; set; }

        public virtual int FeeTypeId { get; set; }

        [ForeignKey("FeeTypeId")]
        public FeeType FeeTypeFk { get; set; }

        public virtual int ProductFeeId { get; set; }

        [ForeignKey("ProductFeeId")]
        public ProductFee ProductFeeFk { get; set; }

    }
}