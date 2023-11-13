using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMSetup.Regions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner.Contract
{
    [Table("PartnerContracts")]
    [Audited]
    public class PartnerContract : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual DateTime ContractExpiryDate { get; set; }

        public virtual decimal CommissionPer { get; set; }

        public virtual long AgentId { get; set; }

        [ForeignKey("AgentId")]
        public Agent AgentFk { get; set; }

        public virtual int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Region RegionFk { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

    }
}