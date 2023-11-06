using Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup.Countries.Dtos;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Regions.Dtos;
using Zeta.AgentosCRM.CRMSetup.Regions;
using Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos;
using Zeta.AgentosCRM.CRMSetup.TaskCategory;
using Zeta.AgentosCRM.CRMSetup.Tag.Dtos;
using Zeta.AgentosCRM.CRMSetup.Tag;
using Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos;
using Zeta.AgentosCRM.CRMSetup.InstallmentType;
using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;
using Zeta.AgentosCRM.CRMSetup.ProductType;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Extensions;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Zeta.AgentosCRM.Auditing.Dto;
using Zeta.AgentosCRM.Authorization.Accounts.Dto;
using Zeta.AgentosCRM.Authorization.Delegation;
using Zeta.AgentosCRM.Authorization.Permissions.Dto;
using Zeta.AgentosCRM.Authorization.Roles;
using Zeta.AgentosCRM.Authorization.Roles.Dto;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Authorization.Users.Delegation.Dto;
using Zeta.AgentosCRM.Authorization.Users.Dto;
using Zeta.AgentosCRM.Authorization.Users.Importing.Dto;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.Chat;
using Zeta.AgentosCRM.Chat.Dto;
using Zeta.AgentosCRM.DynamicEntityProperties.Dto;
using Zeta.AgentosCRM.Editions;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.Friendships;
using Zeta.AgentosCRM.Friendships.Cache;
using Zeta.AgentosCRM.Friendships.Dto;
using Zeta.AgentosCRM.Localization.Dto;
using Zeta.AgentosCRM.MultiTenancy;
using Zeta.AgentosCRM.MultiTenancy.Dto;
using Zeta.AgentosCRM.MultiTenancy.HostDashboard.Dto;
using Zeta.AgentosCRM.MultiTenancy.Payments;
using Zeta.AgentosCRM.MultiTenancy.Payments.Dto;
using Zeta.AgentosCRM.Notifications.Dto;
using Zeta.AgentosCRM.Organizations.Dto;
using Zeta.AgentosCRM.Sessions.Dto;
using Zeta.AgentosCRM.WebHooks.Dto;

namespace Zeta.AgentosCRM
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditCRMCurrencyDto, CRMSetup.CRMCurrency.CRMCurrency>().ReverseMap();
            configuration.CreateMap<CRMCurrencyDto, CRMSetup.CRMCurrency.CRMCurrency>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartnerDto, Partner>().ReverseMap();
            configuration.CreateMap<PartnerDto, Partner>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientTagDto, ClientTag>().ReverseMap();
            configuration.CreateMap<ClientTagDto, ClientTag>().ReverseMap();
            configuration.CreateMap<CreateOrEditFollowerDto, Follower>().ReverseMap();
            configuration.CreateMap<FollowerDto, Follower>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientDto, Client>().ReverseMap();
            configuration.CreateMap<ClientDto, Client>().ReverseMap();
            configuration.CreateMap<CreateOrEditCountryDto, Country>().ReverseMap();
            configuration.CreateMap<CountryDto, Country>().ReverseMap();
            configuration.CreateMap<CreateOrEditRegionDto, Region>().ReverseMap();
            configuration.CreateMap<RegionDto, Region>().ReverseMap();
            configuration.CreateMap<CreateOrEditTaskCategoryDto, CRMSetup.TaskCategory.TaskCategory>().ReverseMap();
            configuration.CreateMap<TaskCategoryDto, CRMSetup.TaskCategory.TaskCategory>().ReverseMap();
            configuration.CreateMap<CreateOrEditTagDto, CRMSetup.Tag.Tag>().ReverseMap();
            configuration.CreateMap<TagDto, CRMSetup.Tag.Tag>().ReverseMap();
            configuration.CreateMap<CreateOrEditInstallmentTypeDto, CRMSetup.InstallmentType.InstallmentType>().ReverseMap();
            configuration.CreateMap<InstallmentTypeDto, CRMSetup.InstallmentType.InstallmentType>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductTypeDto, CRMSetup.ProductType.ProductType>().ReverseMap();
            configuration.CreateMap<ProductTypeDto, CRMSetup.ProductType.ProductType>().ReverseMap();
            configuration.CreateMap<CreateOrEditFeeTypeDto, CRMSetup.FeeType.FeeType>().ReverseMap();
            configuration.CreateMap<FeeTypeDto, CRMSetup.FeeType.FeeType>().ReverseMap();
            configuration.CreateMap<CreateOrEditLeadSourceDto, CRMSetup.LeadSource.LeadSource>().ReverseMap();
            configuration.CreateMap<LeadSourceDto, CRMSetup.LeadSource.LeadSource>().ReverseMap();
            configuration.CreateMap<CreateOrEditServiceCategoryDto, CRMSetup.ServiceCategory.ServiceCategory>().ReverseMap();
            configuration.CreateMap<ServiceCategoryDto, CRMSetup.ServiceCategory.ServiceCategory>().ReverseMap();
            configuration.CreateMap<CreateOrEditDegreeLevelDto, DegreeLevel>().ReverseMap();
            configuration.CreateMap<DegreeLevelDto, DegreeLevel>().ReverseMap();
            configuration.CreateMap<CreateOrEditTaskPriorityDto, TaskPriority>().ReverseMap();
            configuration.CreateMap<TaskPriorityDto, TaskPriority>().ReverseMap();
            configuration.CreateMap<CreateOrEditSubjectDto, Subject>().ReverseMap();
            configuration.CreateMap<SubjectDto, Subject>().ReverseMap();
            configuration.CreateMap<CreateOrEditSubjectAreaDto, SubjectArea>().ReverseMap();
            configuration.CreateMap<SubjectAreaDto, SubjectArea>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkflowDto, Workflow>().ReverseMap();
            configuration.CreateMap<WorkflowDto, Workflow>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkflowStepDto, WorkflowStep>().ReverseMap();
            configuration.CreateMap<WorkflowStepDto, WorkflowStep>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartnerTypeDto, PartnerType>().ReverseMap();
            configuration.CreateMap<PartnerTypeDto, PartnerType>().ReverseMap();
            configuration.CreateMap<CreateOrEditMasterCategoryDto, MasterCategory>().ReverseMap();
            configuration.CreateMap<MasterCategoryDto, MasterCategory>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}