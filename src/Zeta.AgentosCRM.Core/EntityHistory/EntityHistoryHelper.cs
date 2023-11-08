using Zeta.AgentosCRM.CRMClient.Appointment;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMClient;
using System;
using System.Linq;
using Abp.Organizations;
using Zeta.AgentosCRM.Authorization.Roles;
using Zeta.AgentosCRM.MultiTenancy;

namespace Zeta.AgentosCRM.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(ClientAppointment),
            typeof(PartnerContact),
            typeof(Branch),
            typeof(Partner),
            typeof(ClientTag),
            typeof(Follower),
            typeof(Client),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}