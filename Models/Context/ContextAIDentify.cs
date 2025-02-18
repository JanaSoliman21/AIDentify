using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data.Common;

namespace AIDentify.Models.Context
{
    public class ContextAIDentify : IdentityDbContext<ApplicationUser>
    {
        public ContextAIDentify() : base() { }
        public ContextAIDentify(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<XRayScan>()
                .HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<XRayScan>()
                .HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<XRayScan>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(x => x.Quiz)
                .WithMany()
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<Subscription>()
                .HasOne(x => x.PayDate)
                .WithMany()
                .HasForeignKey(x => x.PayDateId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<Subscription>()
                .HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<User>()
                .HasOne(x => x.Subscription)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<User>()
                .HasOne(x => x.Payment)
                .WithMany()
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<XRayScan>())
            {
                var scan = entry.Entity;

                if (!string.IsNullOrEmpty(scan.DoctorId) && string.IsNullOrEmpty(scan.PatientId))
                {
                    throw new Exception("If DoctorId is provided, PatientId must be provided too.");
                }

                if (!string.IsNullOrEmpty(scan.StudentId) && (!string.IsNullOrEmpty(scan.DoctorId) || !string.IsNullOrEmpty(scan.PatientId)))
                {
                    throw new Exception("Student scans must not have DoctorId or PatientId.");
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PayDate> payDate { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<QuizAttempt> QuizAttempt { get; set; }

        public DbSet<Student> Student { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

        public DbSet<SystemUpdate> SystemUpdate { get; set; }

        public DbSet<TrendingNews> TrendingNews { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<XRayScan> XRayScan { get; set; }

    }
}

