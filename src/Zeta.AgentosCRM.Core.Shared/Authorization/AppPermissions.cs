namespace Zeta.AgentosCRM.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_ProductTypes = "Pages.ProductTypes";
        public const string Pages_ProductTypes_Create = "Pages.ProductTypes.Create";
        public const string Pages_ProductTypes_Edit = "Pages.ProductTypes.Edit";
        public const string Pages_ProductTypes_Delete = "Pages.ProductTypes.Delete";

        public const string Pages_FeeTypes = "Pages.FeeTypes";
        public const string Pages_FeeTypes_Create = "Pages.FeeTypes.Create";
        public const string Pages_FeeTypes_Edit = "Pages.FeeTypes.Edit";
        public const string Pages_FeeTypes_Delete = "Pages.FeeTypes.Delete";

        public const string Pages_LeadSources = "Pages.LeadSources";
        public const string Pages_LeadSources_Create = "Pages.LeadSources.Create";
        public const string Pages_LeadSources_Edit = "Pages.LeadSources.Edit";
        public const string Pages_LeadSources_Delete = "Pages.LeadSources.Delete";

        public const string Pages_ServiceCategories = "Pages.ServiceCategories";
        public const string Pages_ServiceCategories_Create = "Pages.ServiceCategories.Create";
        public const string Pages_ServiceCategories_Edit = "Pages.ServiceCategories.Edit";
        public const string Pages_ServiceCategories_Delete = "Pages.ServiceCategories.Delete";

        public const string Pages_DegreeLevels = "Pages.DegreeLevels";
        public const string Pages_DegreeLevels_Create = "Pages.DegreeLevels.Create";
        public const string Pages_DegreeLevels_Edit = "Pages.DegreeLevels.Edit";
        public const string Pages_DegreeLevels_Delete = "Pages.DegreeLevels.Delete";

        public const string Pages_TaskPriorities = "Pages.TaskPriorities";
        public const string Pages_TaskPriorities_Create = "Pages.TaskPriorities.Create";
        public const string Pages_TaskPriorities_Edit = "Pages.TaskPriorities.Edit";
        public const string Pages_TaskPriorities_Delete = "Pages.TaskPriorities.Delete";

        public const string Pages_SubjectAreas = "Pages.SubjectAreas";
        public const string Pages_SubjectAreas_Create = "Pages.SubjectAreas.Create";
        public const string Pages_SubjectAreas_Edit = "Pages.SubjectAreas.Edit";
        public const string Pages_SubjectAreas_Delete = "Pages.SubjectAreas.Delete";

        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";
        public const string Pages_Administration_Users_ChangeProfilePicture = "Pages.Administration.Users.ChangeProfilePicture";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";
        public const string Pages_Administration_Languages_ChangeDefaultLanguage = "Pages.Administration.Languages.ChangeDefaultLanguage";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        public const string Pages_Administration_WebhookSubscription = "Pages.Administration.WebhookSubscription";
        public const string Pages_Administration_WebhookSubscription_Create = "Pages.Administration.WebhookSubscription.Create";
        public const string Pages_Administration_WebhookSubscription_Edit = "Pages.Administration.WebhookSubscription.Edit";
        public const string Pages_Administration_WebhookSubscription_ChangeActivity = "Pages.Administration.WebhookSubscription.ChangeActivity";
        public const string Pages_Administration_WebhookSubscription_Detail = "Pages.Administration.WebhookSubscription.Detail";
        public const string Pages_Administration_Webhook_ListSendAttempts = "Pages.Administration.Webhook.ListSendAttempts";
        public const string Pages_Administration_Webhook_ResendWebhook = "Pages.Administration.Webhook.ResendWebhook";

        public const string Pages_Administration_DynamicProperties = "Pages.Administration.DynamicProperties";
        public const string Pages_Administration_DynamicProperties_Create = "Pages.Administration.DynamicProperties.Create";
        public const string Pages_Administration_DynamicProperties_Edit = "Pages.Administration.DynamicProperties.Edit";
        public const string Pages_Administration_DynamicProperties_Delete = "Pages.Administration.DynamicProperties.Delete";

        public const string Pages_Administration_DynamicPropertyValue = "Pages.Administration.DynamicPropertyValue";
        public const string Pages_Administration_DynamicPropertyValue_Create = "Pages.Administration.DynamicPropertyValue.Create";
        public const string Pages_Administration_DynamicPropertyValue_Edit = "Pages.Administration.DynamicPropertyValue.Edit";
        public const string Pages_Administration_DynamicPropertyValue_Delete = "Pages.Administration.DynamicPropertyValue.Delete";

        public const string Pages_Administration_DynamicEntityProperties = "Pages.Administration.DynamicEntityProperties";
        public const string Pages_Administration_DynamicEntityProperties_Create = "Pages.Administration.DynamicEntityProperties.Create";
        public const string Pages_Administration_DynamicEntityProperties_Edit = "Pages.Administration.DynamicEntityProperties.Edit";
        public const string Pages_Administration_DynamicEntityProperties_Delete = "Pages.Administration.DynamicEntityProperties.Delete";

        public const string Pages_Administration_DynamicEntityPropertyValue = "Pages.Administration.DynamicEntityPropertyValue";
        public const string Pages_Administration_DynamicEntityPropertyValue_Create = "Pages.Administration.DynamicEntityPropertyValue.Create";
        public const string Pages_Administration_DynamicEntityPropertyValue_Edit = "Pages.Administration.DynamicEntityPropertyValue.Edit";
        public const string Pages_Administration_DynamicEntityPropertyValue_Delete = "Pages.Administration.DynamicEntityPropertyValue.Delete";

        public const string Pages_Administration_MassNotification = "Pages.Administration.MassNotification";
        public const string Pages_Administration_MassNotification_Create = "Pages.Administration.MassNotification.Create";

        public const string Pages_Administration_NewVersion_Create = "Pages_Administration_NewVersion_Create";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

        //Added by Raid new Forms
        public const string Pages_CRMSetup = "Pages.CRMSetup";
        public const string Pages_CRMSetup_PartnerTypes = "Pages.CRMSetup.PartnerTypes";
        public const string Pages_CRMSetup_PartnerTypes_Create = "Pages.CRMSetup.PartnerTypes.Create";
        public const string Pages_CRMSetup_PartnerTypes_Edit = "Pages.CRMSetup.PartnerTypes.Edit";
        public const string Pages_CRMSetup_PartnerTypes_Delete = "Pages.CRMSetup.PartnerTypes.Delete";

        public const string Pages_CRMSetup_MasterCategories = "Pages.CRMSetup.MasterCategories";
        public const string Pages_CRMSetup_MasterCategories_Create = "Pages.CRMSetup.MasterCategories.Create";
        public const string Pages_CRMSetup_MasterCategories_Edit = "Pages.CRMSetup.MasterCategories.Edit";
        public const string Pages_CRMSetup_MasterCategories_Delete = "Pages.CRMSetup.MasterCategories.Delete";

        public const string Pages_Subjects = "Pages.Subjects";
        public const string Pages_Subjects_Create = "Pages.Subjects.Create";
        public const string Pages_Subjects_Edit = "Pages.Subjects.Edit";
        public const string Pages_Subjects_Delete = "Pages.Subjects.Delete";

        public const string Pages_Workflows = "Pages.Workflows";
        public const string Pages_Workflows_Create = "Pages.Workflows.Create";
        public const string Pages_Workflows_Edit = "Pages.Workflows.Edit";
        public const string Pages_Workflows_Delete = "Pages.Workflows.Delete";

        public const string Pages_WorkflowSteps = "Pages.WorkflowSteps";
        public const string Pages_WorkflowSteps_Create = "Pages.WorkflowSteps.Create";
        public const string Pages_WorkflowSteps_Edit = "Pages.WorkflowSteps.Edit";
        public const string Pages_WorkflowSteps_Delete = "Pages.WorkflowSteps.Delete";
    }
}