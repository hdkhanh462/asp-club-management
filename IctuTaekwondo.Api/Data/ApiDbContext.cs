using IctuTaekwondo.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Data
{
    public class ApiDbContext : IdentityDbContext<User>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
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
                entity.Property(u => u.Gender).HasConversion<string>();
                entity.Property(u => u.CurrentRank).HasConversion<string>();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasConstraintName("UserProfiles_UserId_FKey");
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRegistrations)
                    .HasConstraintName("EventRegistrations_EventId_FKey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventRegistrations)
                    .HasConstraintName("EventRegistrations_UserId_FKey");
            });

            modelBuilder.Entity<Achievement>(entity =>
            {
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
                entity.Property(u => u.Type).HasConversion<string>();
            });
        }
    }
}
