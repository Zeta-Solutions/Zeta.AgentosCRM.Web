using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Zeta.AgentosCRM.Authorization;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Startup
{
    public class AppAreaNameNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/HostDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Dashboard)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.TestAattachments,
                        L("TestAattachments"),
                        url: "AppAreaName/TestAattachments",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_TestAattachments)
                    )
                )

                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Tenants,
                        L("Tenants"),
                        url: "AppAreaName/Tenants",
                        icon: "flaticon-list-3",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Editions,
                        L("Editions"),
                        url: "AppAreaName/Editions",
                        icon: "flaticon-app",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/TenantDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "AppAreaName/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Roles,
                            L("Roles"),
                            url: "AppAreaName/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Users,
                            L("Users"),
                            url: "AppAreaName/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Users)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Languages,
                            L("Languages"),
                            url: "AppAreaName/Languages",
                            icon: "flaticon-tabs",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Languages)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "AppAreaName/AuditLogs",
                            icon: "flaticon-folder-1",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_AuditLogs)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "AppAreaName/Maintenance",
                            icon: "flaticon-lock",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Maintenance)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.SubscriptionManagement,
                            L("Subscription"),
                            url: "AppAreaName/SubscriptionManagement",
                            icon: "flaticon-refresh",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Tenant_SubscriptionManagement)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.UiCustomization,
                            L("VisualSettings"),
                            url: "AppAreaName/UiCustomization",
                            icon: "flaticon-medical",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_UiCustomization)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.WebhookSubscriptions,
                            L("WebhookSubscriptions"),
                            url: "AppAreaName/WebhookSubscription",
                            icon: "flaticon2-world",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_WebhookSubscription)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.DynamicProperties,
                            L("DynamicProperties"),
                            url: "AppAreaName/DynamicProperty",
                            icon: "flaticon-interface-8",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_DynamicProperties)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.Settings,
                            L("Settings"),
                            url: "AppAreaName/HostSettings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Settings,
                            L("Settings"),
                            url: "AppAreaName/Settings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Tenant_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Notifications,
                            L("Notifications"),
                            icon: "flaticon-alarm"
                        ).AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Notifications_Inbox,
                                L("Inbox"),
                                url: "AppAreaName/Notifications",
                                icon: "flaticon-mail-1"
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Notifications_MassNotifications,
                                L("MassNotifications"),
                                url: "AppAreaName/Notifications/MassNotifications",
                                icon: "flaticon-paper-plane",
                                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_MassNotification)
                            )
                        )
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.DemoUiComponents,
                        L("DemoUiComponents"),
                        url: "AppAreaName/DemoUiComponents",
                        icon: "flaticon-shapes",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                    )
                )

                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.CRMSetup,
                        L("CRMSetup"),
                        icon: "flaticon-settings"
                    ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.MasterCategories,
                        L("MasterCategories"),
                        url: "AppAreaName/MasterCategories",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_CRMSetup_MasterCategories)
                    )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.PartnerTypes,
                            L("PartnerTypes"),
                            url: "AppAreaName/PartnerTypes",
                            icon: "flaticon-more",
                            permissionDependency: new SimplePermissionDependency(
                                AppPermissions.Pages_CRMSetup_PartnerTypes)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.TaskCategory,
                            L("TaskCategory"),
                            url: "AppAreaName/TaskCategory",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_TaskCategories)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Taskpriority,
                            L("TaskPriority"),
                            url: "AppAreaName/Taskpriority",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_TaskPriorities)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.DegreeLevel,
                            L("DegreeLevel"),
                            url: "AppAreaName/DegreeLevel",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DegreeLevels)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.SubjectArea,
                            L("SubjectArea"),
                            url: "AppAreaName/SubjectArea",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_SubjectAreas)
                        )
                    )

                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Subject,
                            L("Subject"),
                            url: "AppAreaName/Subject",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Subjects)
                        )
                    ) 
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.LeadSource,
                            L("LeadSource"),
                            url: "AppAreaName/LeadSource",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_LeadSources)
                        )
                    )

                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.LeadForm,
                            L("Leadform"),
                            url: "AppAreaName/Leadform",
                            icon: "flaticon-more"
                            //,
                            //permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_LeadSources)
                          
                            )
                    )
                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.DocumentChecklist,
                            L("DocumentCheckList"),
                            url: "AppAreaName/DocumentCheckList",
                            icon: "flaticon-more"
                            //,
                            //permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_LeadSources)
                         
                            )
                    )
                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.DocumentType,
                            L("DocumentType"),
                            url: "AppAreaName/DocumentType",
                            icon: "flaticon-more"
                            //,
                            //permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_LeadSources)
                          
                            )
                    )
                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Tag,
                            L("Tag"),
                            url: "AppAreaName/Tag",
                            icon: "flaticon-more",
                             permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tags)

                          )
                     )

                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Country,
                            L("Country"),
                            url: "AppAreaName/Country",
                            icon: "flaticon-more",
                             permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Countries)

                          )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.QuotationCurrency,
                            L("Currency"),
                            url: "AppAreaName/QuotationCurrency",
                            icon: "flaticon-more"
                          )
                    )
                    //.AddItem(new MenuItemDefinition(
                    //        AppAreaNamePageNames.Tenant.Course,
                    //        L("Course"),
                    //        url: "AppAreaName/Course",
                    //        icon: "flaticon-more"
                    //      )
                    //)
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.ServiceCategory,
                            L("ServiceCategory"),
                            url: "AppAreaName/ServiceCategory",
                            icon: "flaticon-more",
                             permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_ServiceCategories)

                          )
                    )
                    .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Workflows,
                        L("Workflows"),
                        url: "AppAreaName/Workflows",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Workflows)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.WorkflowSteps,
                        L("WorkflowSteps"),
                        url: "AppAreaName/WorkflowSteps",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_WorkflowSteps)
                    )
                )
                     .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.InstallmentType,
                            L("InstallmentType"),
                            url: "AppAreaName/InstallmentType",
                            icon: "flaticon-more",
                            permissionDependency: new SimplePermissionDependency(
                                AppPermissions.Pages_InstallmentTypes)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.FeeType,
                            L("FeeType"),
                            url: "AppAreaName/FeeType",
                            icon: "flaticon-more",
                            permissionDependency: new SimplePermissionDependency(
                                AppPermissions.Pages_FeeTypes)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.EmailTemplate,
                            L("EmailTemplate"),
                            url: "AppAreaName/EmailTemplate",
                            icon: "flaticon-more"
                            //,
                            //permissionDependency: new SimplePermissionDependency(
                            //    AppPermissions.Pages_CRMSetup_PartnerTypes)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Regions,
                            L("Regions"),
                            url: "AppAreaName/Regions",
                            icon: "flaticon-more",
                            permissionDependency: new SimplePermissionDependency(
                                AppPermissions.Pages_Regions)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.ProductType,
                            L("ProductType"),
                            url: "AppAreaName/ProductType",
                            icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(
                            AppPermissions.Pages_CRMSetup_PartnerTypes)
                        )
                    )
                )

                    .AddItem(new MenuItemDefinition(
                                    AppAreaNamePageNames.Tenant.Clients,
                                    L("Clients"),
                                    url: "AppAreaName/Clients",
                                    icon: "flaticon-profile-1"
                                // permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_CRMSetup_PartnerTypes)
                                )

                            )

                   .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Partners,
                            L("Partners"),
                            url: "AppAreaName/Partners",
                            icon: "fa-solid fa-handshake-simple",
                        permissionDependency: new SimplePermissionDependency(
                            AppPermissions.Pages_CRMSetup_PartnerTypes)
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Agent,
                        L("Agent"),
                        url: "AppAreaName/Agents",
                        icon: "flaticon-users-1"
                    //,permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Products,
                        L("Products"),
                        url: "AppAreaName/Products",
                        icon: "flaticon-users-1"
                    //,permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                    )
                ); 
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AgentosCRMConsts.LocalizationSourceName);
        }
    }
}