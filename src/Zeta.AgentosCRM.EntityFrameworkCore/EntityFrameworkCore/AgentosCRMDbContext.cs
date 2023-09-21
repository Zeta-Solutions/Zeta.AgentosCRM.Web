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

namespace Zeta.AgentosCRM.EntityFrameworkCore
{
    public class AgentosCRMDbContext : AbpZeroDbContext<Tenant, Role, User, AgentosCRMDbContext>
    {
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