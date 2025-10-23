using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Donor> Donor { get; set; }
        public DbSet<BloodRequest> BloodRequest { get; set; }
        public DbSet<BloodInventory> BloodInventory { get; set; }
        public DbSet<BloodDonation> BloodDonation { get; set; }
        public DbSet<EmailNotification> EmailNotification { get; set; }
        public DbSet<Recipient> Recipient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names explicitly
            modelBuilder.Entity<Donor>().ToTable("DONOR", schema: "dbo");
            modelBuilder.Entity<BloodRequest>().ToTable("BLOODREQUEST", schema: "dbo");
            modelBuilder.Entity<BloodInventory>().ToTable("BLOODINVENTORY", schema: "dbo");
            modelBuilder.Entity<BloodDonation>().ToTable("BLOODDONATION", schema: "dbo");
            modelBuilder.Entity<EmailNotification>().ToTable("EMAILNOTIFICATION", schema: "dbo");
            modelBuilder.Entity<Recipient>().ToTable("RECEIPIENT", schema: "dbo");

            // Configure primary keys
            modelBuilder.Entity<Donor>().HasKey(d => d.DonorId);
            modelBuilder.Entity<BloodRequest>().HasKey(br => br.RequestId);
            modelBuilder.Entity<BloodInventory>().HasKey(bi => bi.UnitId);
            modelBuilder.Entity<BloodDonation>().HasKey(bd => bd.DonationId);
            modelBuilder.Entity<EmailNotification>().HasKey(en => en.NotificationId);
            modelBuilder.Entity<Recipient>().HasKey(r => r.RecipientId);

            // Configure foreign key relationships
            modelBuilder.Entity<BloodDonation>()
                .HasOne(bd => bd.Donor)
                .WithMany(d => d.BloodDonations)
                .HasForeignKey(bd => bd.DonorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BloodRequest>()
                .HasOne(br => br.Recipient)
                .WithMany(r => r.BloodRequests)
                .HasForeignKey(br => br.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmailNotification>()
                .HasOne(en => en.Recipient)
                .WithMany(r => r.EmailNotifications)
                .HasForeignKey(en => en.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure column mappings and identity columns
            modelBuilder.Entity<Donor>()
                .Property(d => d.DonorId)
                .HasColumnName("DONORID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BloodRequest>()
                .Property(br => br.RequestId)
                .HasColumnName("REQUESTID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BloodInventory>()
                .Property(bi => bi.UnitId)
                .HasColumnName("UNITID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BloodDonation>()
                .Property(bd => bd.DonationId)
                .HasColumnName("DONATIONID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmailNotification>()
                .Property(en => en.NotificationId)
                .HasColumnName("NOTIFICATIONID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Recipient>()
                .Property(r => r.RecipientId)
                .HasColumnName("RECEIPIENTID")
                .ValueGeneratedOnAdd();
        }
    }
}
