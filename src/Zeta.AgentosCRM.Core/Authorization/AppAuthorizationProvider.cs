using Abp.Authorization;
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

            var sentEmails = pages.CreateChildPermission(AppPermissions.Pages_SentEmails, L("SentEmails"), multiTenancySides: MultiTenancySides.Tenant);
            sentEmails.CreateChildPermission(AppPermissions.Pages_SentEmails_Create, L("CreateNewSentEmail"), multiTenancySides: MultiTenancySides.Tenant);
            sentEmails.CreateChildPermission(AppPermissions.Pages_SentEmails_Edit, L("EditSentEmail"), multiTenancySides: MultiTenancySides.Tenant);
            sentEmails.CreateChildPermission(AppPermissions.Pages_SentEmails_Delete, L("DeleteSentEmail"), multiTenancySides: MultiTenancySides.Tenant);

            var emailConfigurations = pages.CreateChildPermission(AppPermissions.Pages_EmailConfigurations, L("EmailConfigurations"), multiTenancySides: MultiTenancySides.Tenant);
            emailConfigurations.CreateChildPermission(AppPermissions.Pages_EmailConfigurations_Create, L("CreateNewEmailConfiguration"), multiTenancySides: MultiTenancySides.Tenant);
            emailConfigurations.CreateChildPermission(AppPermissions.Pages_EmailConfigurations_Edit, L("EditEmailConfiguration"), multiTenancySides: MultiTenancySides.Tenant);
            emailConfigurations.CreateChildPermission(AppPermissions.Pages_EmailConfigurations_Delete, L("DeleteEmailConfiguration"), multiTenancySides: MultiTenancySides.Tenant);

            var clientAttachments = pages.CreateChildPermission(AppPermissions.Pages_ClientAttachments, L("ClientAttachments"), multiTenancySides: MultiTenancySides.Tenant);
            clientAttachments.CreateChildPermission(AppPermissions.Pages_ClientAttachments_Create, L("CreateNewClientAttachment"), multiTenancySides: MultiTenancySides.Tenant);
            clientAttachments.CreateChildPermission(AppPermissions.Pages_ClientAttachments_Edit, L("EditClientAttachment"), multiTenancySides: MultiTenancySides.Tenant);
            clientAttachments.CreateChildPermission(AppPermissions.Pages_ClientAttachments_Delete, L("DeleteClientAttachment"), multiTenancySides: MultiTenancySides.Tenant);

            var productBranches = pages.CreateChildPermission(AppPermissions.Pages_ProductBranches, L("ProductBranches"), multiTenancySides: MultiTenancySides.Tenant);
            productBranches.CreateChildPermission(AppPermissions.Pages_ProductBranches_Create, L("CreateNewProductBranch"), multiTenancySides: MultiTenancySides.Tenant);
            productBranches.CreateChildPermission(AppPermissions.Pages_ProductBranches_Edit, L("EditProductBranch"), multiTenancySides: MultiTenancySides.Tenant);
            productBranches.CreateChildPermission(AppPermissions.Pages_ProductBranches_Delete, L("DeleteProductBranch"), multiTenancySides: MultiTenancySides.Tenant);

            var productFeeDetails = pages.CreateChildPermission(AppPermissions.Pages_ProductFeeDetails, L("ProductFeeDetails"), multiTenancySides: MultiTenancySides.Tenant);
            productFeeDetails.CreateChildPermission(AppPermissions.Pages_ProductFeeDetails_Create, L("CreateNewProductFeeDetail"), multiTenancySides: MultiTenancySides.Tenant);
            productFeeDetails.CreateChildPermission(AppPermissions.Pages_ProductFeeDetails_Edit, L("EditProductFeeDetail"), multiTenancySides: MultiTenancySides.Tenant);
            productFeeDetails.CreateChildPermission(AppPermissions.Pages_ProductFeeDetails_Delete, L("DeleteProductFeeDetail"), multiTenancySides: MultiTenancySides.Tenant);

            var productOtherTestRequirements = pages.CreateChildPermission(AppPermissions.Pages_ProductOtherTestRequirements, L("ProductOtherTestRequirements"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherTestRequirements.CreateChildPermission(AppPermissions.Pages_ProductOtherTestRequirements_Create, L("CreateNewProductOtherTestRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherTestRequirements.CreateChildPermission(AppPermissions.Pages_ProductOtherTestRequirements_Edit, L("EditProductOtherTestRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherTestRequirements.CreateChildPermission(AppPermissions.Pages_ProductOtherTestRequirements_Delete, L("DeleteProductOtherTestRequirement"), multiTenancySides: MultiTenancySides.Tenant);

            var productEnglishRequirements = pages.CreateChildPermission(AppPermissions.Pages_ProductEnglishRequirements, L("ProductEnglishRequirements"), multiTenancySides: MultiTenancySides.Tenant);
            productEnglishRequirements.CreateChildPermission(AppPermissions.Pages_ProductEnglishRequirements_Create, L("CreateNewProductEnglishRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productEnglishRequirements.CreateChildPermission(AppPermissions.Pages_ProductEnglishRequirements_Edit, L("EditProductEnglishRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productEnglishRequirements.CreateChildPermission(AppPermissions.Pages_ProductEnglishRequirements_Delete, L("DeleteProductEnglishRequirement"), multiTenancySides: MultiTenancySides.Tenant);

            var productAcadamicRequirements = pages.CreateChildPermission(AppPermissions.Pages_ProductAcadamicRequirements, L("ProductAcadamicRequirements"), multiTenancySides: MultiTenancySides.Tenant);
            productAcadamicRequirements.CreateChildPermission(AppPermissions.Pages_ProductAcadamicRequirements_Create, L("CreateNewProductAcadamicRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productAcadamicRequirements.CreateChildPermission(AppPermissions.Pages_ProductAcadamicRequirements_Edit, L("EditProductAcadamicRequirement"), multiTenancySides: MultiTenancySides.Tenant);
            productAcadamicRequirements.CreateChildPermission(AppPermissions.Pages_ProductAcadamicRequirements_Delete, L("DeleteProductAcadamicRequirement"), multiTenancySides: MultiTenancySides.Tenant);

            var productOtherInformations = pages.CreateChildPermission(AppPermissions.Pages_ProductOtherInformations, L("ProductOtherInformations"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherInformations.CreateChildPermission(AppPermissions.Pages_ProductOtherInformations_Create, L("CreateNewProductOtherInformation"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherInformations.CreateChildPermission(AppPermissions.Pages_ProductOtherInformations_Edit, L("EditProductOtherInformation"), multiTenancySides: MultiTenancySides.Tenant);
            productOtherInformations.CreateChildPermission(AppPermissions.Pages_ProductOtherInformations_Delete, L("DeleteProductOtherInformation"), multiTenancySides: MultiTenancySides.Tenant);

            var productFees = pages.CreateChildPermission(AppPermissions.Pages_ProductFees, L("ProductFees"));
            productFees.CreateChildPermission(AppPermissions.Pages_ProductFees_Create, L("CreateNewProductFee"));
            productFees.CreateChildPermission(AppPermissions.Pages_ProductFees_Edit, L("EditProductFee"));
            productFees.CreateChildPermission(AppPermissions.Pages_ProductFees_Delete, L("DeleteProductFee"));

            var documentCheckListPartners = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners, L("DocumentCheckListPartners"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListPartners.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Create, L("CreateNewDocumentCheckListPartner"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListPartners.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Edit, L("EditDocumentCheckListPartner"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListPartners.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListPartners_Delete, L("DeleteDocumentCheckListPartner"), multiTenancySides: MultiTenancySides.Tenant);

            var documentCheckListProducts = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListProducts, L("DocumentCheckListProducts"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListProducts.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListProducts_Create, L("CreateNewDocumentCheckListProduct"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListProducts.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListProducts_Edit, L("EditDocumentCheckListProduct"), multiTenancySides: MultiTenancySides.Tenant);
            documentCheckListProducts.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentCheckListProducts_Delete, L("DeleteDocumentCheckListProduct"), multiTenancySides: MultiTenancySides.Tenant);

            var workflowStepDocumentCheckLists = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowStepDocumentCheckLists, L("WorkflowStepDocumentCheckLists"), multiTenancySides: MultiTenancySides.Tenant);
            workflowStepDocumentCheckLists.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowStepDocumentCheckLists_Create, L("CreateNewWorkflowStepDocumentCheckList"), multiTenancySides: MultiTenancySides.Tenant);
            workflowStepDocumentCheckLists.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowStepDocumentCheckLists_Edit, L("EditWorkflowStepDocumentCheckList"), multiTenancySides: MultiTenancySides.Tenant);
            workflowStepDocumentCheckLists.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowStepDocumentCheckLists_Delete, L("DeleteWorkflowStepDocumentCheckList"), multiTenancySides: MultiTenancySides.Tenant);

            var workflowDocuments = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowDocuments, L("WorkflowDocuments"), multiTenancySides: MultiTenancySides.Tenant);
            workflowDocuments.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowDocuments_Create, L("CreateNewWorkflowDocument"), multiTenancySides: MultiTenancySides.Tenant);
            workflowDocuments.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowDocuments_Edit, L("EditWorkflowDocument"), multiTenancySides: MultiTenancySides.Tenant);
            workflowDocuments.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowDocuments_Delete, L("DeleteWorkflowDocument"), multiTenancySides: MultiTenancySides.Tenant);

            var taxSettings = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaxSettings, L("TaxSettings"), multiTenancySides: MultiTenancySides.Tenant);
            taxSettings.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaxSettings_Create, L("CreateNewTaxSetting"), multiTenancySides: MultiTenancySides.Tenant);
            taxSettings.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaxSettings_Edit, L("EditTaxSetting"), multiTenancySides: MultiTenancySides.Tenant);
            taxSettings.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaxSettings_Delete, L("DeleteTaxSetting"), multiTenancySides: MultiTenancySides.Tenant);

            var paymentInvoiceTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_PaymentInvoiceTypes, L("PaymentInvoiceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            paymentInvoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PaymentInvoiceTypes_Create, L("CreateNewPaymentInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);
            paymentInvoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PaymentInvoiceTypes_Edit, L("EditPaymentInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);
            paymentInvoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_PaymentInvoiceTypes_Delete, L("DeletePaymentInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);

            var invoiceTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceTypes, L("InvoiceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceTypes_Create, L("CreateNewInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceTypes_Edit, L("EditInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceTypes_Delete, L("DeleteInvoiceType"), multiTenancySides: MultiTenancySides.Tenant);

            var manualPaymentDetails = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_ManualPaymentDetails, L("ManualPaymentDetails"), multiTenancySides: MultiTenancySides.Tenant);
            manualPaymentDetails.CreateChildPermission(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Create, L("CreateNewManualPaymentDetail"), multiTenancySides: MultiTenancySides.Tenant);
            manualPaymentDetails.CreateChildPermission(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Edit, L("EditManualPaymentDetail"), multiTenancySides: MultiTenancySides.Tenant);
            manualPaymentDetails.CreateChildPermission(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Delete, L("DeleteManualPaymentDetail"), multiTenancySides: MultiTenancySides.Tenant);

            var invoiceAddresses = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceAddresses, L("InvoiceAddresses"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceAddresses.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceAddresses_Create, L("CreateNewInvoiceAddress"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceAddresses.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceAddresses_Edit, L("EditInvoiceAddress"), multiTenancySides: MultiTenancySides.Tenant);
            invoiceAddresses.CreateChildPermission(AppPermissions.Pages_CRMSetup_InvoiceAddresses_Delete, L("DeleteInvoiceAddress"), multiTenancySides: MultiTenancySides.Tenant);

            var businessRegNummbers = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_BusinessRegNummbers, L("BusinessRegNummbers"), multiTenancySides: MultiTenancySides.Tenant);
            businessRegNummbers.CreateChildPermission(AppPermissions.Pages_CRMSetup_BusinessRegNummbers_Create, L("CreateNewBusinessRegNummber"), multiTenancySides: MultiTenancySides.Tenant);
            businessRegNummbers.CreateChildPermission(AppPermissions.Pages_CRMSetup_BusinessRegNummbers_Edit, L("EditBusinessRegNummber"), multiTenancySides: MultiTenancySides.Tenant);
            businessRegNummbers.CreateChildPermission(AppPermissions.Pages_CRMSetup_BusinessRegNummbers_Delete, L("DeleteBusinessRegNummber"), multiTenancySides: MultiTenancySides.Tenant);

            var agentContacts = pages.CreateChildPermission(AppPermissions.Pages_AgentContacts, L("AgentContacts"), multiTenancySides: MultiTenancySides.Tenant);
            agentContacts.CreateChildPermission(AppPermissions.Pages_AgentContacts_Create, L("CreateNewAgentContact"), multiTenancySides: MultiTenancySides.Tenant);
            agentContacts.CreateChildPermission(AppPermissions.Pages_AgentContacts_Edit, L("EditAgentContact"), multiTenancySides: MultiTenancySides.Tenant);
            agentContacts.CreateChildPermission(AppPermissions.Pages_AgentContacts_Delete, L("DeleteAgentContact"), multiTenancySides: MultiTenancySides.Tenant);

            var emailTemplates = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_EmailTemplates, L("EmailTemplates"), multiTenancySides: MultiTenancySides.Tenant);
            emailTemplates.CreateChildPermission(AppPermissions.Pages_CRMSetup_EmailTemplates_Create, L("CreateNewEmailTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            emailTemplates.CreateChildPermission(AppPermissions.Pages_CRMSetup_EmailTemplates_Edit, L("EditEmailTemplate"), multiTenancySides: MultiTenancySides.Tenant);
            emailTemplates.CreateChildPermission(AppPermissions.Pages_CRMSetup_EmailTemplates_Delete, L("DeleteEmailTemplate"), multiTenancySides: MultiTenancySides.Tenant);

            var workflowOffices = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowOffices, L("WorkflowOffices"), multiTenancySides: MultiTenancySides.Tenant);
            workflowOffices.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowOffices_Create, L("CreateNewWorkflowOffice"), multiTenancySides: MultiTenancySides.Tenant);
            workflowOffices.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowOffices_Edit, L("EditWorkflowOffice"), multiTenancySides: MultiTenancySides.Tenant);
            workflowOffices.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowOffices_Delete, L("DeleteWorkflowOffice"), multiTenancySides: MultiTenancySides.Tenant);

            var documentTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentTypes, L("DocumentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            documentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentTypes_Create, L("CreateNewDocumentType"), multiTenancySides: MultiTenancySides.Tenant);
            documentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentTypes_Edit, L("EditDocumentType"), multiTenancySides: MultiTenancySides.Tenant);
            documentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_DocumentTypes_Delete, L("DeleteDocumentType"), multiTenancySides: MultiTenancySides.Tenant);

            var clientQuotationDetails = pages.CreateChildPermission(AppPermissions.Pages_ClientQuotationDetails, L("ClientQuotationDetails"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationDetails.CreateChildPermission(AppPermissions.Pages_ClientQuotationDetails_Create, L("CreateNewClientQuotationDetail"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationDetails.CreateChildPermission(AppPermissions.Pages_ClientQuotationDetails_Edit, L("EditClientQuotationDetail"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationDetails.CreateChildPermission(AppPermissions.Pages_ClientQuotationDetails_Delete, L("DeleteClientQuotationDetail"), multiTenancySides: MultiTenancySides.Tenant);

            var clientQuotationHeads = pages.CreateChildPermission(AppPermissions.Pages_ClientQuotationHeads, L("ClientQuotationHeads"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationHeads.CreateChildPermission(AppPermissions.Pages_ClientQuotationHeads_Create, L("CreateNewClientQuotationHead"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationHeads.CreateChildPermission(AppPermissions.Pages_ClientQuotationHeads_Edit, L("EditClientQuotationHead"), multiTenancySides: MultiTenancySides.Tenant);
            clientQuotationHeads.CreateChildPermission(AppPermissions.Pages_ClientQuotationHeads_Delete, L("DeleteClientQuotationHead"), multiTenancySides: MultiTenancySides.Tenant);

            var checkInLogs = pages.CreateChildPermission(AppPermissions.Pages_CheckInLogs, L("CheckInLogs"), multiTenancySides: MultiTenancySides.Tenant);
            checkInLogs.CreateChildPermission(AppPermissions.Pages_CheckInLogs_Create, L("CreateNewCheckInLog"), multiTenancySides: MultiTenancySides.Tenant);
            checkInLogs.CreateChildPermission(AppPermissions.Pages_CheckInLogs_Edit, L("EditCheckInLog"), multiTenancySides: MultiTenancySides.Tenant);
            checkInLogs.CreateChildPermission(AppPermissions.Pages_CheckInLogs_Delete, L("DeleteCheckInLog"), multiTenancySides: MultiTenancySides.Tenant);

            var otherTestScores = pages.CreateChildPermission(AppPermissions.Pages_OtherTestScores, L("OtherTestScores"), multiTenancySides: MultiTenancySides.Tenant);
            otherTestScores.CreateChildPermission(AppPermissions.Pages_OtherTestScores_Create, L("CreateNewOtherTestScore"), multiTenancySides: MultiTenancySides.Tenant);
            otherTestScores.CreateChildPermission(AppPermissions.Pages_OtherTestScores_Edit, L("EditOtherTestScore"), multiTenancySides: MultiTenancySides.Tenant);
            otherTestScores.CreateChildPermission(AppPermissions.Pages_OtherTestScores_Delete, L("DeleteOtherTestScore"), multiTenancySides: MultiTenancySides.Tenant);

            var englisTestScores = pages.CreateChildPermission(AppPermissions.Pages_EnglisTestScores, L("EnglisTestScores"), multiTenancySides: MultiTenancySides.Tenant);
            englisTestScores.CreateChildPermission(AppPermissions.Pages_EnglisTestScores_Create, L("CreateNewEnglisTestScore"), multiTenancySides: MultiTenancySides.Tenant);
            englisTestScores.CreateChildPermission(AppPermissions.Pages_EnglisTestScores_Edit, L("EditEnglisTestScore"), multiTenancySides: MultiTenancySides.Tenant);
            englisTestScores.CreateChildPermission(AppPermissions.Pages_EnglisTestScores_Delete, L("DeleteEnglisTestScore"), multiTenancySides: MultiTenancySides.Tenant);

            var promotionProducts = pages.CreateChildPermission(AppPermissions.Pages_PromotionProducts, L("PromotionProducts"), multiTenancySides: MultiTenancySides.Tenant);
            promotionProducts.CreateChildPermission(AppPermissions.Pages_PromotionProducts_Create, L("CreateNewPromotionProduct"), multiTenancySides: MultiTenancySides.Tenant);
            promotionProducts.CreateChildPermission(AppPermissions.Pages_PromotionProducts_Edit, L("EditPromotionProduct"), multiTenancySides: MultiTenancySides.Tenant);
            promotionProducts.CreateChildPermission(AppPermissions.Pages_PromotionProducts_Delete, L("DeletePromotionProduct"), multiTenancySides: MultiTenancySides.Tenant);

            var taskFollowers = pages.CreateChildPermission(AppPermissions.Pages_TaskFollowers, L("TaskFollowers"), multiTenancySides: MultiTenancySides.Tenant);
            taskFollowers.CreateChildPermission(AppPermissions.Pages_TaskFollowers_Create, L("CreateNewTaskFollower"), multiTenancySides: MultiTenancySides.Tenant);
            taskFollowers.CreateChildPermission(AppPermissions.Pages_TaskFollowers_Edit, L("EditTaskFollower"), multiTenancySides: MultiTenancySides.Tenant);
            taskFollowers.CreateChildPermission(AppPermissions.Pages_TaskFollowers_Delete, L("DeleteTaskFollower"), multiTenancySides: MultiTenancySides.Tenant);

            var clientInterstedServices = pages.CreateChildPermission(AppPermissions.Pages_ClientInterstedServices, L("ClientInterstedServices"), multiTenancySides: MultiTenancySides.Tenant);
            clientInterstedServices.CreateChildPermission(AppPermissions.Pages_ClientInterstedServices_Create, L("CreateNewClientInterstedService"), multiTenancySides: MultiTenancySides.Tenant);
            clientInterstedServices.CreateChildPermission(AppPermissions.Pages_ClientInterstedServices_Edit, L("EditClientInterstedService"), multiTenancySides: MultiTenancySides.Tenant);
            clientInterstedServices.CreateChildPermission(AppPermissions.Pages_ClientInterstedServices_Delete, L("DeleteClientInterstedService"), multiTenancySides: MultiTenancySides.Tenant);

            var appointmentInvitees = pages.CreateChildPermission(AppPermissions.Pages_AppointmentInvitees, L("AppointmentInvitees"), multiTenancySides: MultiTenancySides.Tenant);
            appointmentInvitees.CreateChildPermission(AppPermissions.Pages_AppointmentInvitees_Create, L("CreateNewAppointmentInvitee"), multiTenancySides: MultiTenancySides.Tenant);
            appointmentInvitees.CreateChildPermission(AppPermissions.Pages_AppointmentInvitees_Edit, L("EditAppointmentInvitee"), multiTenancySides: MultiTenancySides.Tenant);
            appointmentInvitees.CreateChildPermission(AppPermissions.Pages_AppointmentInvitees_Delete, L("DeleteAppointmentInvitee"), multiTenancySides: MultiTenancySides.Tenant);

            var appointments = pages.CreateChildPermission(AppPermissions.Pages_Appointments, L("Appointments"), multiTenancySides: MultiTenancySides.Tenant);
            appointments.CreateChildPermission(AppPermissions.Pages_Appointments_Create, L("CreateNewAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            appointments.CreateChildPermission(AppPermissions.Pages_Appointments_Edit, L("EditAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            appointments.CreateChildPermission(AppPermissions.Pages_Appointments_Delete, L("DeleteAppointment"), multiTenancySides: MultiTenancySides.Tenant);

            var crmTasks = pages.CreateChildPermission(AppPermissions.Pages_CRMTasks, L("CRMTasks"), multiTenancySides: MultiTenancySides.Tenant);
            crmTasks.CreateChildPermission(AppPermissions.Pages_CRMTasks_Create, L("CreateNewCRMTask"), multiTenancySides: MultiTenancySides.Tenant);
            crmTasks.CreateChildPermission(AppPermissions.Pages_CRMTasks_Edit, L("EditCRMTask"), multiTenancySides: MultiTenancySides.Tenant);
            crmTasks.CreateChildPermission(AppPermissions.Pages_CRMTasks_Delete, L("DeleteCRMTask"), multiTenancySides: MultiTenancySides.Tenant);

            var applicationStages = pages.CreateChildPermission(AppPermissions.Pages_ApplicationStages, L("ApplicationStages"), multiTenancySides: MultiTenancySides.Tenant);
            applicationStages.CreateChildPermission(AppPermissions.Pages_ApplicationStages_Create, L("CreateNewApplicationStage"), multiTenancySides: MultiTenancySides.Tenant);
            applicationStages.CreateChildPermission(AppPermissions.Pages_ApplicationStages_Edit, L("EditApplicationStage"), multiTenancySides: MultiTenancySides.Tenant);
            applicationStages.CreateChildPermission(AppPermissions.Pages_ApplicationStages_Delete, L("DeleteApplicationStage"), multiTenancySides: MultiTenancySides.Tenant);

            var applications = pages.CreateChildPermission(AppPermissions.Pages_Applications, L("Applications"), multiTenancySides: MultiTenancySides.Tenant);
            applications.CreateChildPermission(AppPermissions.Pages_Applications_Create, L("CreateNewApplication"), multiTenancySides: MultiTenancySides.Tenant);
            applications.CreateChildPermission(AppPermissions.Pages_Applications_Edit, L("EditApplication"), multiTenancySides: MultiTenancySides.Tenant);
            applications.CreateChildPermission(AppPermissions.Pages_Applications_Delete, L("DeleteApplication"), multiTenancySides: MultiTenancySides.Tenant);

            var products = pages.CreateChildPermission(AppPermissions.Pages_Products, L("Products"), multiTenancySides: MultiTenancySides.Tenant);
            products.CreateChildPermission(AppPermissions.Pages_Products_Create, L("CreateNewProduct"), multiTenancySides: MultiTenancySides.Tenant);
            products.CreateChildPermission(AppPermissions.Pages_Products_Edit, L("EditProduct"), multiTenancySides: MultiTenancySides.Tenant);
            products.CreateChildPermission(AppPermissions.Pages_Products_Delete, L("DeleteProduct"), multiTenancySides: MultiTenancySides.Tenant);

            var clientEducations = pages.CreateChildPermission(AppPermissions.Pages_ClientEducations, L("ClientEducations"), multiTenancySides: MultiTenancySides.Tenant);
            clientEducations.CreateChildPermission(AppPermissions.Pages_ClientEducations_Create, L("CreateNewClientEducation"), multiTenancySides: MultiTenancySides.Tenant);
            clientEducations.CreateChildPermission(AppPermissions.Pages_ClientEducations_Edit, L("EditClientEducation"), multiTenancySides: MultiTenancySides.Tenant);
            clientEducations.CreateChildPermission(AppPermissions.Pages_ClientEducations_Delete, L("DeleteClientEducation"), multiTenancySides: MultiTenancySides.Tenant);

            var partnerContracts = pages.CreateChildPermission(AppPermissions.Pages_PartnerContracts, L("PartnerContracts"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContracts.CreateChildPermission(AppPermissions.Pages_PartnerContracts_Create, L("CreateNewPartnerContract"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContracts.CreateChildPermission(AppPermissions.Pages_PartnerContracts_Edit, L("EditPartnerContract"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContracts.CreateChildPermission(AppPermissions.Pages_PartnerContracts_Delete, L("DeletePartnerContract"), multiTenancySides: MultiTenancySides.Tenant);

            var notes = pages.CreateChildPermission(AppPermissions.Pages_Notes, L("Notes"), multiTenancySides: MultiTenancySides.Tenant);
            notes.CreateChildPermission(AppPermissions.Pages_Notes_Create, L("CreateNewNote"), multiTenancySides: MultiTenancySides.Tenant);
            notes.CreateChildPermission(AppPermissions.Pages_Notes_Edit, L("EditNote"), multiTenancySides: MultiTenancySides.Tenant);
            notes.CreateChildPermission(AppPermissions.Pages_Notes_Delete, L("DeleteNote"), multiTenancySides: MultiTenancySides.Tenant);

            var agents = pages.CreateChildPermission(AppPermissions.Pages_Agents, L("Agents"), multiTenancySides: MultiTenancySides.Tenant);
            agents.CreateChildPermission(AppPermissions.Pages_Agents_Create, L("CreateNewAgent"), multiTenancySides: MultiTenancySides.Tenant);
            agents.CreateChildPermission(AppPermissions.Pages_Agents_Edit, L("EditAgent"), multiTenancySides: MultiTenancySides.Tenant);
            agents.CreateChildPermission(AppPermissions.Pages_Agents_Delete, L("DeleteAgent"), multiTenancySides: MultiTenancySides.Tenant);

            var testAattachments = pages.CreateChildPermission(AppPermissions.Pages_TestAattachments, L("TestAattachments"), multiTenancySides: MultiTenancySides.Host);
            testAattachments.CreateChildPermission(AppPermissions.Pages_TestAattachments_Create, L("CreateNewTestAattachment"), multiTenancySides: MultiTenancySides.Host);
            testAattachments.CreateChildPermission(AppPermissions.Pages_TestAattachments_Edit, L("EditTestAattachment"), multiTenancySides: MultiTenancySides.Host);
            testAattachments.CreateChildPermission(AppPermissions.Pages_TestAattachments_Delete, L("DeleteTestAattachment"), multiTenancySides: MultiTenancySides.Host);

            var partnerPromotions = pages.CreateChildPermission(AppPermissions.Pages_PartnerPromotions, L("PartnerPromotions"), multiTenancySides: MultiTenancySides.Tenant);
            partnerPromotions.CreateChildPermission(AppPermissions.Pages_PartnerPromotions_Create, L("CreateNewPartnerPromotion"), multiTenancySides: MultiTenancySides.Tenant);
            partnerPromotions.CreateChildPermission(AppPermissions.Pages_PartnerPromotions_Edit, L("EditPartnerPromotion"), multiTenancySides: MultiTenancySides.Tenant);
            partnerPromotions.CreateChildPermission(AppPermissions.Pages_PartnerPromotions_Delete, L("DeletePartnerPromotion"), multiTenancySides: MultiTenancySides.Tenant);

            var clientAppointments = pages.CreateChildPermission(AppPermissions.Pages_ClientAppointments, L("ClientAppointments"), multiTenancySides: MultiTenancySides.Tenant);
            clientAppointments.CreateChildPermission(AppPermissions.Pages_ClientAppointments_Create, L("CreateNewClientAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            clientAppointments.CreateChildPermission(AppPermissions.Pages_ClientAppointments_Edit, L("EditClientAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            clientAppointments.CreateChildPermission(AppPermissions.Pages_ClientAppointments_Delete, L("DeleteClientAppointment"), multiTenancySides: MultiTenancySides.Tenant);

            var partnerContacts = pages.CreateChildPermission(AppPermissions.Pages_PartnerContacts, L("PartnerContacts"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContacts.CreateChildPermission(AppPermissions.Pages_PartnerContacts_Create, L("CreateNewPartnerContact"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContacts.CreateChildPermission(AppPermissions.Pages_PartnerContacts_Edit, L("EditPartnerContact"), multiTenancySides: MultiTenancySides.Tenant);
            partnerContacts.CreateChildPermission(AppPermissions.Pages_PartnerContacts_Delete, L("DeletePartnerContact"), multiTenancySides: MultiTenancySides.Tenant);

            var branches = pages.CreateChildPermission(AppPermissions.Pages_Branches, L("Branches"), multiTenancySides: MultiTenancySides.Tenant);
            branches.CreateChildPermission(AppPermissions.Pages_Branches_Create, L("CreateNewBranch"), multiTenancySides: MultiTenancySides.Tenant);
            branches.CreateChildPermission(AppPermissions.Pages_Branches_Edit, L("EditBranch"), multiTenancySides: MultiTenancySides.Tenant);
            branches.CreateChildPermission(AppPermissions.Pages_Branches_Delete, L("DeleteBranch"), multiTenancySides: MultiTenancySides.Tenant);

            var crmCurrencies = pages.CreateChildPermission(AppPermissions.Pages_CRMCurrencies, L("CRMCurrencies"), multiTenancySides: MultiTenancySides.Tenant);
            crmCurrencies.CreateChildPermission(AppPermissions.Pages_CRMCurrencies_Create, L("CreateNewCRMCurrency"), multiTenancySides: MultiTenancySides.Tenant);
            crmCurrencies.CreateChildPermission(AppPermissions.Pages_CRMCurrencies_Edit, L("EditCRMCurrency"), multiTenancySides: MultiTenancySides.Tenant);
            crmCurrencies.CreateChildPermission(AppPermissions.Pages_CRMCurrencies_Delete, L("DeleteCRMCurrency"), multiTenancySides: MultiTenancySides.Tenant);

            var partners = pages.CreateChildPermission(AppPermissions.Pages_Partners, L("Partners"), multiTenancySides: MultiTenancySides.Tenant);
            partners.CreateChildPermission(AppPermissions.Pages_Partners_Create, L("CreateNewPartner"), multiTenancySides: MultiTenancySides.Tenant);
            partners.CreateChildPermission(AppPermissions.Pages_Partners_Edit, L("EditPartner"), multiTenancySides: MultiTenancySides.Tenant);
            partners.CreateChildPermission(AppPermissions.Pages_Partners_Delete, L("DeletePartner"), multiTenancySides: MultiTenancySides.Tenant);

            var clientTags = pages.CreateChildPermission(AppPermissions.Pages_ClientTags, L("ClientTags"), multiTenancySides: MultiTenancySides.Tenant);
            clientTags.CreateChildPermission(AppPermissions.Pages_ClientTags_Create, L("CreateNewClientTag"), multiTenancySides: MultiTenancySides.Tenant);
            clientTags.CreateChildPermission(AppPermissions.Pages_ClientTags_Edit, L("EditClientTag"), multiTenancySides: MultiTenancySides.Tenant);
            clientTags.CreateChildPermission(AppPermissions.Pages_ClientTags_Delete, L("DeleteClientTag"), multiTenancySides: MultiTenancySides.Tenant);

            var followers = pages.CreateChildPermission(AppPermissions.Pages_Followers, L("Followers"), multiTenancySides: MultiTenancySides.Tenant);
            followers.CreateChildPermission(AppPermissions.Pages_Followers_Create, L("CreateNewFollower"), multiTenancySides: MultiTenancySides.Tenant);
            followers.CreateChildPermission(AppPermissions.Pages_Followers_Edit, L("EditFollower"), multiTenancySides: MultiTenancySides.Tenant);
            followers.CreateChildPermission(AppPermissions.Pages_Followers_Delete, L("DeleteFollower"), multiTenancySides: MultiTenancySides.Tenant);

            var clients = pages.CreateChildPermission(AppPermissions.Pages_Clients, L("Clients"), multiTenancySides: MultiTenancySides.Tenant);
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Create, L("CreateNewClient"), multiTenancySides: MultiTenancySides.Tenant);
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Edit, L("EditClient"), multiTenancySides: MultiTenancySides.Tenant);
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Delete, L("DeleteClient"), multiTenancySides: MultiTenancySides.Tenant);

            var countries = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_Countries, L("Countries"), multiTenancySides: MultiTenancySides.Tenant);
            countries.CreateChildPermission(AppPermissions.Pages_CRMSetup_Countries_Create, L("CreateNewCountry"), multiTenancySides: MultiTenancySides.Tenant);
            countries.CreateChildPermission(AppPermissions.Pages_CRMSetup_Countries_Edit, L("EditCountry"), multiTenancySides: MultiTenancySides.Tenant);
            countries.CreateChildPermission(AppPermissions.Pages_CRMSetup_Countries_Delete, L("DeleteCountry"), multiTenancySides: MultiTenancySides.Tenant);

            var regions = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_Regions, L("Regions"), multiTenancySides: MultiTenancySides.Tenant);
            regions.CreateChildPermission(AppPermissions.Pages_CRMSetup_Regions_Create, L("CreateNewRegion"), multiTenancySides: MultiTenancySides.Tenant);
            regions.CreateChildPermission(AppPermissions.Pages_CRMSetup_Regions_Edit, L("EditRegion"), multiTenancySides: MultiTenancySides.Tenant);
            regions.CreateChildPermission(AppPermissions.Pages_CRMSetup_Regions_Delete, L("DeleteRegion"), multiTenancySides: MultiTenancySides.Tenant);

            var taskCategories = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskCategories, L("TaskCategories"), multiTenancySides: MultiTenancySides.Tenant);
            taskCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskCategories_Create, L("CreateNewTaskCategory"), multiTenancySides: MultiTenancySides.Tenant);
            taskCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskCategories_Edit, L("EditTaskCategory"), multiTenancySides: MultiTenancySides.Tenant);
            taskCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskCategories_Delete, L("DeleteTaskCategory"), multiTenancySides: MultiTenancySides.Tenant);

            var tags = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_Tags, L("Tags"));
            tags.CreateChildPermission(AppPermissions.Pages_CRMSetup_Tags_Create, L("CreateNewTag"));
            tags.CreateChildPermission(AppPermissions.Pages_CRMSetup_Tags_Edit, L("EditTag"));
            tags.CreateChildPermission(AppPermissions.Pages_CRMSetup_Tags_Delete, L("DeleteTag"));

            var installmentTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_InstallmentTypes, L("InstallmentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            installmentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InstallmentTypes_Create, L("CreateNewInstallmentType"), multiTenancySides: MultiTenancySides.Tenant);
            installmentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InstallmentTypes_Edit, L("EditInstallmentType"), multiTenancySides: MultiTenancySides.Tenant);
            installmentTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_InstallmentTypes_Delete, L("DeleteInstallmentType"), multiTenancySides: MultiTenancySides.Tenant);

            var productTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_ProductTypes, L("ProductTypes"), multiTenancySides: MultiTenancySides.Tenant);
            productTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_ProductTypes_Create, L("CreateNewProductType"), multiTenancySides: MultiTenancySides.Tenant);
            productTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_ProductTypes_Edit, L("EditProductType"), multiTenancySides: MultiTenancySides.Tenant);
            productTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_ProductTypes_Delete, L("DeleteProductType"), multiTenancySides: MultiTenancySides.Tenant);

            var feeTypes = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_FeeTypes, L("FeeTypes"), multiTenancySides: MultiTenancySides.Tenant);
            feeTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_FeeTypes_Create, L("CreateNewFeeType"), multiTenancySides: MultiTenancySides.Tenant);
            feeTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_FeeTypes_Edit, L("EditFeeType"), multiTenancySides: MultiTenancySides.Tenant);
            feeTypes.CreateChildPermission(AppPermissions.Pages_CRMSetup_FeeTypes_Delete, L("DeleteFeeType"), multiTenancySides: MultiTenancySides.Tenant);

            var leadSources = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_LeadSources, L("LeadSources"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_CRMSetup_LeadSources_Create, L("CreateNewLeadSource"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_CRMSetup_LeadSources_Edit, L("EditLeadSource"), multiTenancySides: MultiTenancySides.Tenant);
            leadSources.CreateChildPermission(AppPermissions.Pages_CRMSetup_LeadSources_Delete, L("DeleteLeadSource"), multiTenancySides: MultiTenancySides.Tenant);

            var serviceCategories = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_ServiceCategories, L("ServiceCategories"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_ServiceCategories_Create, L("CreateNewServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_ServiceCategories_Edit, L("EditServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);
            serviceCategories.CreateChildPermission(AppPermissions.Pages_CRMSetup_ServiceCategories_Delete, L("DeleteServiceCategory"), multiTenancySides: MultiTenancySides.Tenant);

            var degreeLevels = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_DegreeLevels, L("DegreeLevels"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_CRMSetup_DegreeLevels_Create, L("CreateNewDegreeLevel"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_CRMSetup_DegreeLevels_Edit, L("EditDegreeLevel"));
            degreeLevels.CreateChildPermission(AppPermissions.Pages_CRMSetup_DegreeLevels_Delete, L("DeleteDegreeLevel"));

            var taskPriorities = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskPriorities, L("TaskPriorities"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskPriorities_Create, L("CreateNewTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskPriorities_Edit, L("EditTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);
            taskPriorities.CreateChildPermission(AppPermissions.Pages_CRMSetup_TaskPriorities_Delete, L("DeleteTaskPriority"), multiTenancySides: MultiTenancySides.Tenant);

            var subjectAreas = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_SubjectAreas, L("SubjectAreas"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_CRMSetup_SubjectAreas_Create, L("CreateNewSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_CRMSetup_SubjectAreas_Edit, L("EditSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);
            subjectAreas.CreateChildPermission(AppPermissions.Pages_CRMSetup_SubjectAreas_Delete, L("DeleteSubjectArea"), multiTenancySides: MultiTenancySides.Tenant);

            var subjects = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_Subjects, L("Subjects"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_CRMSetup_Subjects_Create, L("CreateNewSubject"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_CRMSetup_Subjects_Edit, L("EditSubject"), multiTenancySides: MultiTenancySides.Tenant);
            subjects.CreateChildPermission(AppPermissions.Pages_CRMSetup_Subjects_Delete, L("DeleteSubject"), multiTenancySides: MultiTenancySides.Tenant);

            var workflows = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_Workflows, L("Workflows"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_CRMSetup_Workflows_Create, L("CreateNewWorkflow"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_CRMSetup_Workflows_Edit, L("EditWorkflow"), multiTenancySides: MultiTenancySides.Tenant);
            workflows.CreateChildPermission(AppPermissions.Pages_CRMSetup_Workflows_Delete, L("DeleteWorkflow"), multiTenancySides: MultiTenancySides.Tenant);

            var workflowSteps = pages.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowSteps, L("WorkflowSteps"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowSteps_Create, L("CreateNewWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowSteps_Edit, L("EditWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);
            workflowSteps.CreateChildPermission(AppPermissions.Pages_CRMSetup_WorkflowSteps_Delete, L("DeleteWorkflowStep"), multiTenancySides: MultiTenancySides.Tenant);

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