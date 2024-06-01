﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Procoding.ApplicationTracker.Infrastructure.Data;

#nullable disable

namespace Procoding.ApplicationTracker.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240601132154_addedIdentityForCandidatesAndCreatedEmployeesTable")]
    partial class addedIdentityForCandidatesAndCreatedEmployeesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Procoding.ApplicationTracker.Domain.Entities.Candidate.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("nvarchar(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Candidates", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("CompanyName", "Procoding.ApplicationTracker.Domain.Entities.Company.CompanyName#CompanyName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("OfficialWebSiteLink", "Procoding.ApplicationTracker.Domain.Entities.Company.OfficialWebSiteLink#Link", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("nvarchar(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.CompanyAverageGrossSalary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GrossSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyAverageGrossSalaries", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Procoding.ApplicationTracker.Domain.Entities.Employee.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("nvarchar(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.InterviewStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("InteviewStepType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JobApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("JobApplicationId");

                    b.ToTable("InterviewSteps", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationSourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppliedOnUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobApplicationStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationSourceId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobApplications", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.JobApplicationSource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("JobApplicationSources", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.CompanyAverageGrossSalary", b =>
                {
                    b.HasOne("Procoding.ApplicationTracker.Domain.Entities.Company", "Company")
                        .WithMany("CompanyAverageGrossSalaries")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.InterviewStep", b =>
                {
                    b.HasOne("Procoding.ApplicationTracker.Domain.Entities.JobApplication", "JobApplication")
                        .WithMany("InterviewSteps")
                        .HasForeignKey("JobApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobApplication");
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.JobApplication", b =>
                {
                    b.HasOne("Procoding.ApplicationTracker.Domain.Entities.JobApplicationSource", "ApplicationSource")
                        .WithMany()
                        .HasForeignKey("ApplicationSourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Procoding.ApplicationTracker.Domain.Entities.Candidate", "Candidate")
                        .WithMany("JobApplications")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Procoding.ApplicationTracker.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationSource");

                    b.Navigation("Candidate");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Candidate", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Company", b =>
                {
                    b.Navigation("CompanyAverageGrossSalaries");
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.JobApplication", b =>
                {
                    b.Navigation("InterviewSteps");
                });
#pragma warning restore 612, 618
        }
    }
}
