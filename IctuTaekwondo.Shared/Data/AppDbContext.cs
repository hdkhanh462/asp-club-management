using IctuTaekwondo.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Shared.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Finance> Finances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("UserProfiles_PKey");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(u => u.Gender).HasConversion<string>();
                entity.Property(u => u.CurrentRank).HasConversion<string>();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasConstraintName("UserProfiles_UserId_FKey");

            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Events_PKey");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("EventRegistrations_PKey");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRegistrations)
                    .HasConstraintName("EventRegistrations_EventId_FKey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventRegistrations)
                    .HasConstraintName("EventRegistrations_UserId_FKey");
            });

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Achievements_PKey");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Achievements)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Achievements_EventId_FKey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AchievementUsers)
                    .HasConstraintName("Achievements_UserId_FKey");
            });

            modelBuilder.Entity<Finance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Finances_PKey");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(u => u.Type).HasConversion<string>();
            });
        }
    }
}
