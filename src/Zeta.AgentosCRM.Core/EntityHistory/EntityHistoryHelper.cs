using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.OtherInfo;
using Zeta.AgentosCRM.CRMProducts.Fee;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMAgent.Contacts;
using Zeta.AgentosCRM.CRMClient.Quotation;
using Zeta.AgentosCRM.CRMClient.CheckIn;
using Zeta.AgentosCRM.TaskManagement.Followers;
using Zeta.AgentosCRM.CRMClient.InterstedServices;
using Zeta.AgentosCRM.CRMAppointments.Invitees;
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.TaskManagement;
using Zeta.AgentosCRM.CRMApplications.Stages;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMClient.Education;
using Zeta.AgentosCRM.CRMPartner.Contract;
using Zeta.AgentosCRM.CRMNotes;
using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMPartner.Promotion;
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
            typeof(ProductFee),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(ProductBranch),
            typeof(ProductFeeDetail),
            typeof(ProductOtherTestRequirement),
            typeof(ProductEnglishRequirement),
            typeof(ProductAcadamicRequirement),
            typeof(ProductOtherInformation),
            typeof(ProductFee),
            typeof(ManualPaymentDetail),
            typeof(InvoiceAddress),
            typeof(BusinessRegNummber),
            typeof(AgentContact),
            typeof(ClientQuotationDetail),
            typeof(ClientQuotationHead),
            typeof(CheckInLog),
            typeof(OtherTestScore),
            typeof(EnglisTestScore),
            typeof(PromotionProduct),
            typeof(TaskFollower),
            typeof(ClientInterstedService),
            typeof(AppointmentInvitee),
            typeof(Appointment),
            typeof(CRMTask),
            typeof(ApplicationStage),
            typeof(Application),
            typeof(Product),
            typeof(ClientEducation),
            typeof(PartnerContract),
            typeof(Note),
            typeof(Agent),
            typeof(PartnerPromotion),
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