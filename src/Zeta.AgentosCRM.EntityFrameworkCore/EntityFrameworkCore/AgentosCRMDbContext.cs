﻿using Zeta.AgentosCRM.CRMPartner;
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
using PayPalCheckoutSdk.Orders;

namespace Zeta.AgentosCRM.EntityFrameworkCore
{
    public class AgentosCRMDbContext : AbpZeroDbContext<Tenant, Role, User, AgentosCRMDbContext>
    {
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

        public virtual DbSet<Invoice> Invoices { get; set; }

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