﻿// <auto-generated />
using System;
using AIDentify.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIDentify.Migrations
{
    [DbContext(typeof(ContextAIDentify))]
    [Migration("20250310222709_ExpirationFirstEdit")]
    partial class ExpirationFirstEdit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AIDentify.Models.Admin", b =>
                {
                    b.Property<string>("Admin_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Admin_ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("AIDentify.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ClinicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("University")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AIDentify.Models.Doctor", b =>
                {
                    b.Property<string>("Doctor_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClinicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Doctor_ID");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("AIDentify.Models.MedicalHistory", b =>
                {
                    b.Property<string>("MedicalHistoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TeethCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("XRay")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("MedicalHistoryId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalHistory");
                });

            modelBuilder.Entity("AIDentify.Models.Message", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverIdD")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReceiverIdS")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderIdD")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderIdS")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverIdD");

                    b.HasIndex("ReceiverIdS");

                    b.HasIndex("SenderIdD");

                    b.HasIndex("SenderIdS");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("AIDentify.Models.Patient", b =>
                {
                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("AIDentify.Models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WayOfPayment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("AIDentify.Models.Plan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("MaxPatients")
                        .HasColumnType("int");

                    b.Property<int>("MaxScans")
                        .HasColumnType("int");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("AIDentify.Models.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Options")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TheQuestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("AIDentify.Models.Quiz", b =>
                {
                    b.Property<string>("QuizId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("QuizId");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("AIDentify.Models.QuizAttempt", b =>
                {
                    b.Property<string>("QuizAttemptId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PointsEarned")
                        .HasColumnType("int");

                    b.Property<string>("QuizId")
                        .HasColumnType("nvarchar(450)");

                    b.PrimitiveCollection<string>("SelectedAnswers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("QuizAttemptId");

                    b.HasIndex("QuizId");

                    b.HasIndex("StudentId");

                    b.ToTable("QuizAttempt");
                });

            modelBuilder.Entity("AIDentify.Models.Student", b =>
                {
                    b.Property<string>("Student_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("TotalPOintsEarned")
                        .HasColumnType("bigint");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Student_ID");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AIDentify.Models.Subscription", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("PlanId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("WarningDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId")
                        .IsUnique()
                        .HasFilter("[DoctorId] IS NOT NULL");

                    b.HasIndex("PlanId");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("AIDentify.Models.SystemUpdate", b =>
                {
                    b.Property<string>("SystemUpdateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UpdateType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedDescribtion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SystemUpdateId");

                    b.HasIndex("AdminId");

                    b.ToTable("SystemUpdate");
                });

            modelBuilder.Entity("AIDentify.Models.TrendingNews", b =>
                {
                    b.Property<string>("NewsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsId");

                    b.ToTable("TrendingNews");
                });

            modelBuilder.Entity("AIDentify.Models.XRayScan", b =>
                {
                    b.Property<string>("ScanId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("DiseasePrediction")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PredictedAgeGroup")
                        .HasColumnType("int");

                    b.Property<int>("PredictedGenderGroup")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScanDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TeethCount")
                        .HasColumnType("int");

                    b.HasKey("ScanId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("StudentId");

                    b.ToTable("XRayScan");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AIDentify.Models.Doctor", b =>
                {
                    b.HasOne("AIDentify.Models.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("AIDentify.Models.MedicalHistory", b =>
                {
                    b.HasOne("AIDentify.Models.Patient", "Patient")
                        .WithMany("MedicalHistories")
                        .HasForeignKey("PatientId");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("AIDentify.Models.Message", b =>
                {
                    b.HasOne("AIDentify.Models.Doctor", "ReceiverDoctor")
                        .WithMany()
                        .HasForeignKey("ReceiverIdD")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Student", "ReceiverStudent")
                        .WithMany()
                        .HasForeignKey("ReceiverIdS")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Doctor", "SenderDoctor")
                        .WithMany()
                        .HasForeignKey("SenderIdD")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Student", "SenderStudent")
                        .WithMany()
                        .HasForeignKey("SenderIdS")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ReceiverDoctor");

                    b.Navigation("ReceiverStudent");

                    b.Navigation("SenderDoctor");

                    b.Navigation("SenderStudent");
                });

            modelBuilder.Entity("AIDentify.Models.Patient", b =>
                {
                    b.HasOne("AIDentify.Models.Doctor", "Doctor")
                        .WithMany("patients")
                        .HasForeignKey("DoctorId");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("AIDentify.Models.Payment", b =>
                {
                    b.HasOne("AIDentify.Models.Doctor", "Doctor")
                        .WithMany("Payments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Student", "Student")
                        .WithMany("Payments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Doctor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AIDentify.Models.Question", b =>
                {
                    b.HasOne("AIDentify.Models.Quiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("AIDentify.Models.QuizAttempt", b =>
                {
                    b.HasOne("AIDentify.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Quiz");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AIDentify.Models.Student", b =>
                {
                    b.HasOne("AIDentify.Models.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("AIDentify.Models.Subscription", b =>
                {
                    b.HasOne("AIDentify.Models.Doctor", "Doctor")
                        .WithOne()
                        .HasForeignKey("AIDentify.Models.Subscription", "DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId");

                    b.HasOne("AIDentify.Models.Student", "Student")
                        .WithOne()
                        .HasForeignKey("AIDentify.Models.Subscription", "StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Doctor");

                    b.Navigation("Plan");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AIDentify.Models.SystemUpdate", b =>
                {
                    b.HasOne("AIDentify.Models.Admin", "Admin")
                        .WithMany("SystemUpdates")
                        .HasForeignKey("AdminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("AIDentify.Models.XRayScan", b =>
                {
                    b.HasOne("AIDentify.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIDentify.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AIDentify.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AIDentify.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIDentify.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AIDentify.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AIDentify.Models.Admin", b =>
                {
                    b.Navigation("SystemUpdates");
                });

            modelBuilder.Entity("AIDentify.Models.Doctor", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("patients");
                });

            modelBuilder.Entity("AIDentify.Models.Patient", b =>
                {
                    b.Navigation("MedicalHistories");
                });

            modelBuilder.Entity("AIDentify.Models.Quiz", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("AIDentify.Models.Student", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
