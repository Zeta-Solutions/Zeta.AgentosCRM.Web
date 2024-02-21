using Zeta.AgentosCRM.CRMLeadInquiry;
using Zeta.AgentosCRM.Tenants.Email.Configuration;
using Zeta.AgentosCRM.CRMClient.Documents;
using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.OtherInfo;
using Zeta.AgentosCRM.CRMProducts.Fee;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMAgent.Contacts;
using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.CRMSetup.Documents;
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
using Zeta.AgentosCRM.AttachmentTest;
using Zeta.AgentosCRM.CRMPartner.Promotion;
using Zeta.AgentosCRM.CRMClient.Appointment;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Regions;
using Zeta.AgentosCRM.CRMSetup.TaskCategory;
using Zeta.AgentosCRM.CRMSetup.Tag;
using Zeta.AgentosCRM.CRMSetup.InstallmentType;
using Zeta.AgentosCRM.CRMSetup.ProductType;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory;
using Zeta.AgentosCRM.CRMSetup;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zeta.AgentosCRM.Authorization.Delegation;
using Zeta.AgentosCRM.Authorization.Roles;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Chat;
using Zeta.AgentosCRM.Editions;
using Zeta.AgentosCRM.Friendships;
using Zeta.AgentosCRM.MultiTenancy;
using Zeta.AgentosCRM.MultiTenancy.Accounting;
using Zeta.AgentosCRM.MultiTenancy.Payments;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.Tenants.Email;
using Zeta.AgentosCRM.CRMLead;
using Zeta.AgentosCRM.CRMInvoice;

namespace Zeta.AgentosCRM.EntityFrameworkCore
{
    public class AgentosCRMDbContext : AbpZeroDbContext<Tenant, Role, User, AgentosCRMDbContext>
    {
        public virtual DbSet<InvoiceHead> InvoiceHeads { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<CRMInquiry> CRMInquiries { get; set; }

        public virtual DbSet<LeadHead> LeadHeads { get; set; }
        public virtual DbSet<LeadDetail> LeadDetails { get; set; }
        public virtual DbSet<SentEmail> SentEmails { get; set; }

        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }

        public virtual DbSet<ClientAttachment> ClientAttachments { get; set; }

        public virtual DbSet<ProductBranch> ProductBranches { get; set; }

        public virtual DbSet<ProductFeeDetail> ProductFeeDetails { get; set; }

        public virtual DbSet<ProductOtherTestRequirement> ProductOtherTestRequirements { get; set; }

        public virtual DbSet<ProductEnglishRequirement> ProductEnglishRequirements { get; set; }

        public virtual DbSet<ProductAcadamicRequirement> ProductAcadamicRequirements { get; set; }

        public virtual DbSet<ProductOtherInformation> ProductOtherInformations { get; set; }

        public virtual DbSet<ProductFee> ProductFees { get; set; }

        public virtual DbSet<DocumentCheckListPartner> DocumentCheckListPartners { get; set; }

        public virtual DbSet<DocumentCheckListProduct> DocumentCheckListProducts { get; set; }

        public virtual DbSet<WorkflowStepDocumentCheckList> WorkflowStepDocumentCheckLists { get; set; }

        public virtual DbSet<WorkflowDocument> WorkflowDocuments { get; set; }

        public virtual DbSet<TaxSetting> TaxSettings { get; set; }

        public virtual DbSet<PaymentInvoiceType> PaymentInvoiceTypes { get; set; }

        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }

        public virtual DbSet<ManualPaymentDetail> ManualPaymentDetails { get; set; }

        public virtual DbSet<InvoiceAddress> InvoiceAddresses { get; set; }

        public virtual DbSet<BusinessRegNummber> BusinessRegNummbers { get; set; }

        public virtual DbSet<AgentContact> AgentContacts { get; set; }

        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

        public virtual DbSet<WorkflowOffice> WorkflowOffices { get; set; }

        public virtual DbSet<DocumentType> DocumentTypes { get; set; }

        public virtual DbSet<ClientQuotationDetail> ClientQuotationDetails { get; set; }

        public virtual DbSet<ClientQuotationHead> ClientQuotationHeads { get; set; }

        public virtual DbSet<CheckInLog> CheckInLogs { get; set; }

        public virtual DbSet<OtherTestScore> OtherTestScores { get; set; }

        public virtual DbSet<EnglisTestScore> EnglisTestScores { get; set; }

        public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }

        public virtual DbSet<TaskFollower> TaskFollowers { get; set; }

        public virtual DbSet<ClientInterstedService> ClientInterstedServices { get; set; }

        public virtual DbSet<AppointmentInvitee> AppointmentInvitees { get; set; }

        public virtual DbSet<Appointment> Appointments { get; set; }

        public virtual DbSet<CRMTask> CRMTasks { get; set; }

        public virtual DbSet<ApplicationStage> ApplicationStages { get; set; }

        public virtual DbSet<Application> Applications { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ClientEducation> ClientEducations { get; set; }

        public virtual DbSet<PartnerContract> PartnerContracts { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public virtual DbSet<Agent> Agents { get; set; }

        public virtual DbSet<TestAattachment> TestAattachments { get; set; }

        public virtual DbSet<PartnerPromotion> PartnerPromotions { get; set; }

        public virtual DbSet<ClientAppointment> ClientAppointments { get; set; }

        public virtual DbSet<PartnerContact> PartnerContacts { get; set; }

        public virtual DbSet<CRMPartner.PartnerBranch.Branch> Branches { get; set; }

        public virtual DbSet<CRMSetup.CRMCurrency.CRMCurrency> CRMCurrencies { get; set; }

        public virtual DbSet<Partner> Partners { get; set; }

        public virtual DbSet<ClientTag> ClientTags { get; set; }

        public virtual DbSet<Follower> Followers { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<CRMSetup.TaskCategory.TaskCategory> TaskCategories { get; set; }

        public virtual DbSet<CRMSetup.Tag.Tag> Tags { get; set; }

        public virtual DbSet<CRMSetup.InstallmentType.InstallmentType> InstallmentTypes { get; set; }

        public virtual DbSet<CRMSetup.ProductType.ProductType> ProductTypes { get; set; }

        public virtual DbSet<CRMSetup.FeeType.FeeType> FeeTypes { get; set; }

        public virtual DbSet<CRMSetup.LeadSource.LeadSource> LeadSources { get; set; }

        public virtual DbSet<CRMSetup.ServiceCategory.ServiceCategory> ServiceCategories { get; set; }

        public virtual DbSet<DegreeLevel> DegreeLevels { get; set; }

        public virtual DbSet<TaskPriority> TaskPriorities { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<SubjectArea> SubjectAreas { get; set; }

        public virtual DbSet<Workflow> Workflows { get; set; }

        public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

        public virtual DbSet<PartnerType> PartnerTypes { get; set; }

        public virtual DbSet<MasterCategory> MasterCategories { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

       // public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        public AgentosCRMDbContext(DbContextOptions<AgentosCRMDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceDetail>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<InvoiceHead>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<CRMInquiry>(c =>
            {
                c.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<LeadDetail>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<LeadHead>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<SentEmail>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<EmailConfiguration>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientAttachment>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductBranch>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductFeeDetail>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductFee>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductOtherTestRequirement>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductEnglishRequirement>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductAcadamicRequirement>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductOtherInformation>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductFee>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<DocumentCheckListPartner>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<DocumentCheckListProduct>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkflowStepDocumentCheckList>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkflowDocument>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<TaxSetting>(t =>
                       {
                           t.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PaymentInvoiceType>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<InvoiceType>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ManualPaymentDetail>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<InvoiceAddress>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BusinessRegNummber>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<AgentContact>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EmailTemplate>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkflowOffice>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<DocumentType>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientQuotationDetail>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientQuotationHead>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<CheckInLog>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<OtherTestScore>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EnglisTestScore>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PromotionProduct>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<TaskFollower>(t =>
                       {
                           t.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientInterstedService>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientEducation>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<AppointmentInvitee>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Appointment>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<CRMTask>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ApplicationStage>(a =>
                                  {
                                      a.HasIndex(e => new { e.TenantId });
                                  });
            modelBuilder.Entity<Application>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Product>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientEducation>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PartnerContract>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Note>(n =>
                       {
                           n.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Agent>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PartnerPromotion>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PartnerContact>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientAppointment>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PartnerContact>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Branch>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Partner>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Client>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<CRMCurrency>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Client>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Partner>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClientTag>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Follower>(f =>
                       {
                           f.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Client>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Country>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Region>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<TaskCategory>(t =>
                                  {
                                      t.HasIndex(e => new { e.TenantId });
                                  });
            modelBuilder.Entity<Tag>(t =>
                       {
                           t.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ProductType>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<InstallmentType>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ProductType>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<FeeType>(f =>
                       {
                           f.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<LeadSource>(l =>
                       {
                           l.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ServiceCategory>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<DegreeLevel>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<TaskPriority>(t =>
                       {
                           t.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Subject>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<SubjectArea>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkflowStep>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Workflow>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkflowStep>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<PartnerType>(p =>
                       {
                           p.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MasterCategory>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });
        }
    }
}