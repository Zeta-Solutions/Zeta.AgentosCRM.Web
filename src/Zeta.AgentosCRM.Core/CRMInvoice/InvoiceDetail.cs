using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMSetup.Account;

namespace Zeta.AgentosCRM.CRMInvoice
{
    [Table("InvoiceDetail")]
    [Audited]
    public class InvoiceDetail : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public  virtual string Description { get; set; }
        public  virtual decimal? TotalFee { get; set; }
        public  virtual decimal? CommissionPercent { get; set; }
        public  virtual decimal? CommissionAmount { get; set; }
        public virtual int? Tax { get; set; }
        [ForeignKey("TaxSettings")]
        public TaxSetting TaxSettingFk { get; set; }
        public  virtual decimal? TaxAmount { get; set; }
        public virtual decimal? NetAmount { get; set; }
        public virtual int? IncomeType { get; set; }
        [ForeignKey("InvoiceTypes")]
        public InvoiceType InvoiceTypesFk { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual long InvoiceHeadId { get; set; }
    }
}
