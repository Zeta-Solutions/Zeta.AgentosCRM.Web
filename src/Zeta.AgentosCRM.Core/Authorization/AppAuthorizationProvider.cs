﻿using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Zeta.AgentosCRM.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var leadSources = pages.CreateChildPermission(AppPermissions.Pages_LeadSources, L("LeadSources"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_LeadSources_Create, L("CreateNewLeadSource"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_LeadSources_Edit, L("EditLeadSource"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_LeadSources_Delete, L("DeleteLeadSource"), multiTenancySides: MultiTenancySides.Tenant);

            var serviceCategories = pages.CreateChildPermission(AppPermissions.Pages_ServiceCategories, L("ServiceCategories"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_ServiceCategories_Create, L("CreateNewServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_ServiceCategories_Edit, L("EditServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_ServiceCategories_Delete, L("DeleteServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);

            var degreeLevels = pages.CreateChildPermission(AppPermissions.Pages_DegreeLevels, L("DegreeLevels"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_DegreeLevels_Create, L("CreateNewDegreeLevel"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_DegreeLevels_Edit, L("EditDegreeLevel"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_DegreeLevels_Delete, L("DeleteDegreeLevel"));

            var taskPriorities = pages.CreateChildPermission(AppPermissions.Pages_TaskPriorities, L("TaskPriorities"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_TaskPriorities_Create, L("CreateNewTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_TaskPriorities_Edit, L("EditTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_TaskPriorities_Delete, L("DeleteTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);

            var subjectAreas = pages.CreateChildPermission(AppPermissions.Pages_SubjectAreas, L("SubjectAreas"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_SubjectAreas_Create, L("CreateNewSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_SubjectAreas_Edit, L("EditSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_SubjectAreas_Delete, L("DeleteSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);

            var subjects = pages.CreateChildPermission(AppPermissions.Pages_Subjects, L("Subjects"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_Subjects_Create, L("CreateNewSubject"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_Subjects_Edit, L("EditSubject"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_Subjects_Delete, L("DeleteSubject"), multiTenancySides: MultiTenancySides.Tenant);

            var workflows = pages.CreateChildPermission(AppPermissions.Pages_Workflows, L("Workflows"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_Workflows_Create, L("CreateNewWorkflow"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_Workflows_Edit, L("EditWorkflow"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_Workflows_Delete, L("DeleteWorkflow"), multiTenancySides: MultiTenancySides.Tenant);

            var workflowSteps = pages.CreateChildPermission(AppPermissions.Pages_WorkflowSteps, L("WorkflowSteps"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_WorkflowSteps_Create, L("CreateNewWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_WorkflowSteps_Edit, L("EditWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_WorkflowSteps_Delete, L("DeleteWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);

            var CRMSetup = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup, L("CRMSetup"));

            var partnerTypes = CRMSetup.CreateChildPermission(AppPermissions.Pages_CRMSetup_PartnerTypes, L("PartnerTypes"), multiTenancySides: MultiTenancySides.Tenant);
            partnerTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PartnerTypes_Create, L("CreateNewPartnerType"), multiTenancySides: MultiTenancySides.Tenant);
            partnerTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PartnerTypes_Edit, L("EditPartnerType"), multiTenancySides: MultiTenancySides.Tenant);
            partnerTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PartnerTypes_Delete, L("DeletePartnerType"), multiTenancySides: MultiTenancySides.Tenant);

            var masterCategories = CRMSetup.CreateChildPermission(AppPermissions.Pages_CRMSetup_MasterCategories, L("MasterCategories"), multiTenancySides: MultiTenancySides.Tenant);
            masterCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_MasterCategories_Create, L("CreateNewMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);
            masterCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_MasterCategories_Edit, L("EditMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);
            masterCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_MasterCategories_Delete, L("DeleteMasterCategory"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangeProfilePicture, L("UpdateUsersProfilePicture"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeDefaultLanguage, L("ChangeDefaultLanguage"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            var massNotification = administration.CreateChildPermission(AppPermissions.Pages_Administration_MassNotification, L("MassNotifications"));
            massNotification.CreateChildPermission(AppPermissions.Pages_Administration_MassNotification_Create, L("MassNotificationCreate"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);

            var maintenance = administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            maintenance.CreateChildPermission(AppPermissions.Pages_Administration_NewVersion_Create, L("SendNewVersionNotification"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AgentosCRMConsts.LocalizationSourceName);
        }
    }
}