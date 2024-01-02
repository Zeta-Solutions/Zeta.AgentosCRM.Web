using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;
using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos;
using Zeta.AgentosCRM.CRMProducts.OtherInfo;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.CRMProducts.Fee;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMAgent.Contacts.Dtos;
using Zeta.AgentosCRM.CRMAgent.Contacts;
using Zeta.AgentosCRM.CRMSetup.Email.Dtos;
using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.CRMSetup.Documents;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation;
using Zeta.AgentosCRM.CRMClient.Profile.Dto;
using Zeta.AgentosCRM.CRMClient.Profile;
using Zeta.AgentosCRM.CRMClient.CheckIn.Dtos;
using Zeta.AgentosCRM.CRMClient.CheckIn;
using Zeta.AgentosCRM.TaskManagement.Followers.Dtos;
using Zeta.AgentosCRM.TaskManagement.Followers;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;
using Zeta.AgentosCRM.CRMClient.InterstedServices;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.CRMAppointments.Invitees;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.TaskManagement;
using Zeta.AgentosCRM.CRMApplications.Stages.Dtos;
using Zeta.AgentosCRM.CRMApplications.Stages;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.CRMClient.Education;
using Zeta.AgentosCRM.CRMPartner.Contract.Dtos;
using Zeta.AgentosCRM.CRMPartner.Contract;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.CRMNotes;
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.AttachmentTest.Dtos;
using Zeta.AgentosCRM.AttachmentTest;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.CRMPartner.Promotion;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.CRMClient.Appointment;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
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
            configuration.CreateMap<CreateOrEditProductBranchDto, ProductBranch>().ReverseMap();
            configuration.CreateMap<ProductBranchDto, ProductBranch>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductFeeDetailDto, ProductFeeDetail>().ReverseMap();
            configuration.CreateMap<ProductFeeDetailDto, ProductFeeDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductOtherTestRequirementDto, ProductOtherTestRequirement>().ReverseMap();
            configuration.CreateMap<ProductOtherTestRequirementDto, ProductOtherTestRequirement>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductEnglishRequirementDto, ProductEnglishRequirement>().ReverseMap();
            configuration.CreateMap<ProductEnglishRequirementDto, ProductEnglishRequirement>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductAcadamicRequirementDto, ProductAcadamicRequirement>().ReverseMap();
            configuration.CreateMap<ProductAcadamicRequirementDto, ProductAcadamicRequirement>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductOtherInformationDto, ProductOtherInformation>().ReverseMap();
            configuration.CreateMap<ProductOtherInformationDto, ProductOtherInformation>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductFeeDto, ProductFee>().ReverseMap();
            configuration.CreateMap<ProductFeeDto, ProductFee>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentCheckListPartnerDto, DocumentCheckListPartner>().ReverseMap();
            configuration.CreateMap<DocumentCheckListPartnerDto, DocumentCheckListPartner>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentCheckListProductDto, DocumentCheckListProduct>().ReverseMap();
            configuration.CreateMap<DocumentCheckListProductDto, DocumentCheckListProduct>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkflowStepDocumentCheckListDto, WorkflowStepDocumentCheckList>().ReverseMap();
            configuration.CreateMap<WorkflowStepDocumentCheckListDto, WorkflowStepDocumentCheckList>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkflowDocumentDto, WorkflowDocument>().ReverseMap();
            configuration.CreateMap<WorkflowDocumentDto, WorkflowDocument>().ReverseMap();
            configuration.CreateMap<CreateOrEditTaxSettingDto, TaxSetting>().ReverseMap();
            configuration.CreateMap<TaxSettingDto, TaxSetting>().ReverseMap();
            configuration.CreateMap<CreateOrEditPaymentInvoiceTypeDto, PaymentInvoiceType>().ReverseMap();
            configuration.CreateMap<PaymentInvoiceTypeDto, PaymentInvoiceType>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvoiceTypeDto, InvoiceType>().ReverseMap();
            configuration.CreateMap<InvoiceTypeDto, InvoiceType>().ReverseMap();
            configuration.CreateMap<CreateOrEditManualPaymentDetailDto, ManualPaymentDetail>().ReverseMap();
            configuration.CreateMap<ManualPaymentDetailDto, ManualPaymentDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvoiceAddressDto, InvoiceAddress>().ReverseMap();
            configuration.CreateMap<InvoiceAddressDto, InvoiceAddress>().ReverseMap();
            configuration.CreateMap<CreateOrEditBusinessRegNummberDto, BusinessRegNummber>().ReverseMap();
            configuration.CreateMap<BusinessRegNummberDto, BusinessRegNummber>().ReverseMap();
            configuration.CreateMap<CreateOrEditAgentContactDto, AgentContact>().ReverseMap();
            configuration.CreateMap<AgentContactDto, AgentContact>().ReverseMap();
            configuration.CreateMap<CreateOrEditEmailTemplateDto, EmailTemplate>().ReverseMap();
            configuration.CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkflowOfficeDto, WorkflowOffice>().ReverseMap();
            configuration.CreateMap<WorkflowOfficeDto, WorkflowOffice>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentTypeDto, DocumentType>().ReverseMap();
            configuration.CreateMap<DocumentTypeDto, DocumentType>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientQuotationDetailDto, ClientQuotationDetail>().ReverseMap();
            configuration.CreateMap<ClientQuotationDetailDto, ClientQuotationDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientQuotationHeadDto, ClientQuotationHead>().ReverseMap();
            configuration.CreateMap<ClientQuotationHeadDto, ClientQuotationHead>().ReverseMap();
            configuration.CreateMap<CreateOrEditCheckInLogDto, CheckInLog>().ReverseMap();
            configuration.CreateMap<CheckInLogDto, CheckInLog>().ReverseMap();
            configuration.CreateMap<CreateOrEditOtherTestScoreDto, OtherTestScore>().ReverseMap();
            configuration.CreateMap<OtherTestScoreDto, OtherTestScore>().ReverseMap();
            configuration.CreateMap<CreateOrEditEnglisTestScoreDto, EnglisTestScore>().ReverseMap();
            configuration.CreateMap<EnglisTestScoreDto, EnglisTestScore>().ReverseMap();
            configuration.CreateMap<CreateOrEditPromotionProductDto, PromotionProduct>().ReverseMap();
            configuration.CreateMap<PromotionProductDto, PromotionProduct>().ReverseMap();
            configuration.CreateMap<CreateOrEditTaskFollowerDto, TaskFollower>().ReverseMap();
            configuration.CreateMap<TaskFollowerDto, TaskFollower>().ReverseMap();
            configuration.CreateMap<UpdateClientProfilePictureInput, Client>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientInterstedServiceDto, ClientInterstedService>().ReverseMap();
            configuration.CreateMap<ClientInterstedServiceDto, ClientInterstedService>().ReverseMap();
            configuration.CreateMap<CreateOrEditAppointmentInviteeDto, AppointmentInvitee>().ReverseMap();
            configuration.CreateMap<AppointmentInviteeDto, AppointmentInvitee>().ReverseMap();
            configuration.CreateMap<CreateOrEditAppointmentDto, Appointment>().ReverseMap();
            configuration.CreateMap<AppointmentDto, Appointment>().ReverseMap();
            configuration.CreateMap<CreateOrEditCRMTaskDto, CRMTask>().ReverseMap();
            configuration.CreateMap<CRMTaskDto, CRMTask>().ReverseMap();
            configuration.CreateMap<CreateOrEditApplicationStageDto, ApplicationStage>().ReverseMap();
            configuration.CreateMap<ApplicationStageDto, ApplicationStage>().ReverseMap();
            configuration.CreateMap<CreateOrEditApplicationDto, Application>().ReverseMap();
            configuration.CreateMap<ApplicationDto, Application>().ReverseMap();
            configuration.CreateMap<CreateOrEditProductDto, Product>().ReverseMap();
            configuration.CreateMap<ProductDto, Product>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientEducationDto, ClientEducation>().ReverseMap();
            configuration.CreateMap<ClientEducationDto, ClientEducation>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartnerContractDto, PartnerContract>().ReverseMap();
            configuration.CreateMap<PartnerContractDto, PartnerContract>().ReverseMap();
            configuration.CreateMap<CreateOrEditNoteDto, Note>().ReverseMap();
            configuration.CreateMap<NoteDto, Note>().ReverseMap();
            configuration.CreateMap<CreateOrEditAgentDto, Agent>().ReverseMap();
            configuration.CreateMap<AgentDto, Agent>().ReverseMap();
            configuration.CreateMap<CreateOrEditTestAattachmentDto, TestAattachment>().ReverseMap();
            configuration.CreateMap<TestAattachmentDto, TestAattachment>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartnerPromotionDto, PartnerPromotion>().ReverseMap();
            configuration.CreateMap<PartnerPromotionDto, PartnerPromotion>().ReverseMap();
            configuration.CreateMap<CreateOrEditClientAppointmentDto, ClientAppointment>().ReverseMap();
            configuration.CreateMap<ClientAppointmentDto, ClientAppointment>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartnerContactDto, PartnerContact>().ReverseMap();
            configuration.CreateMap<PartnerContactDto, PartnerContact>().ReverseMap();
            configuration.CreateMap<CreateOrEditBranchDto, CRMPartner.PartnerBranch.Branch>().ReverseMap();
            configuration.CreateMap<BranchDto, CRMPartner.PartnerBranch.Branch>().ReverseMap();
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
            configuration.CreateMap<EntityPropertyChange, CutomizedEntityChangeAndUserDto>();

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