using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [Table("BusinessRegNummbers")]
    [Audited]
    public class BusinessRegNummber : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string RegistrationNo { get; set; }

        public virtual long OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }

    }
}