﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            // XRayScan Relationships - Enforce Rules
            modelBuilder.Entity<XRayScan>()
                .HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<XRayScan>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade); 

            // QuizAttempt Relationships
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(x => x.Quiz)
                .WithMany()
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subscription
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Doctor)
                .WithOne()
                .HasForeignKey<Subscription>(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Student)
                .WithOne()
                .HasForeignKey<Subscription>(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(s => s.Doctor)
                .WithMany(n => n.Notifications)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(s => s.Student)
                .WithMany(n => n.Notifications)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // SystemUpdate
            modelBuilder.Entity<SystemUpdate>()
                .HasOne(s => s.Admin)
                .WithMany()
                .HasForeignKey(s => s.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Payments)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            //MedicalHistory
            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.XRayScan)
                .WithMany()
                .HasForeignKey(m => m.XRayScanId)
                .OnDelete(DeleteBehavior.Cascade);

            //Patient
            modelBuilder.Entity<Patient>()
              .HasOne(p => p.Doctor)
              .WithMany(d =>d.patients)
              .HasForeignKey(p => p.DoctorId)
              .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }



        public override int SaveChanges()
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries<XRayScan>())
                {
                    var scan = entry.Entity;

                    if (!string.IsNullOrEmpty(scan.DoctorId))
                    {
                        throw new Exception("If DoctorId is provided, PatientId must be provided too.");
                    }

                    if (!string.IsNullOrEmpty(scan.StudentId) && (!string.IsNullOrEmpty(scan.DoctorId)))
                    {
                        throw new Exception("Student scans must not have DoctorId or PatientId.");
                    }
                }

                foreach (var entry in ChangeTracker.Entries<Subscription>())
                {
                    var sub = entry.Entity;

                    if (!string.IsNullOrEmpty(sub.DoctorId) && !string.IsNullOrEmpty(sub.StudentId))
                    {
                        throw new Exception("A Subscription must belong to either a Doctor or a Student, not both.");
                    }
                }

                foreach (var entry in ChangeTracker.Entries<Notification>())
                {
                    var sub = entry.Entity;

                    if (!string.IsNullOrEmpty(sub.DoctorId) && !string.IsNullOrEmpty(sub.StudentId))
                    {
                        throw new Exception("A Notification must belong to either a Doctor or a Student, not both.");
                    }
                }

                foreach (var entry in ChangeTracker.Entries<Payment>())
                {
                    var payment = entry.Entity;

                    if (!string.IsNullOrEmpty(payment.DoctorId) && !string.IsNullOrEmpty(payment.StudentId))
                    {
                        throw new Exception("A Payment must belong to either a Doctor or a Student, not both.");
                    }
                }

                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveChanges: {ex.Message}");
                throw;
            }
        }


        public DbSet<Admin> Admin { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuizAttempt> QuizAttempt { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<SystemUpdate> SystemUpdate { get; set; }
        public DbSet<XRayScan> XRayScan { get; set; }
        public DbSet<Notification> Notification { get; set; }

    }
}

