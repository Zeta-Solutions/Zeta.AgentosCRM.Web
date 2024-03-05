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
using Abp.Organizations;

namespace Zeta.AgentosCRM.CRMInvoice
{
    [Table("InvIncomeSharing")]
    [Audited]
    public class InvIncomeSharing : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual long? InvoiceHeadId { get; set; }
        public virtual long? PaymentsReceivedId { get; set; }
        public virtual decimal? IncomeSharing { get; set; }
        public virtual bool? IsTax { get; set; }
        public virtual int? Tax { get; set; }
        [ForeignKey("TaxSettings")]
        public TaxSetting TaxSettingFk { get; set; }
        public virtual decimal? TaxAmount { get; set; }
        public virtual decimal? TotalIncludingTax { get; set; }
        public virtual long? OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }
    }
}
