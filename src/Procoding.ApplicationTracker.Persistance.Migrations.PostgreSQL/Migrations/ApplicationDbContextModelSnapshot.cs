﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Procoding.ApplicationTracker.Infrastructure.Data;

#nullable disable

namespace Procoding.ApplicationTracker.Persistance.Migrations.PostgreSQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Auth.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.HasKey("Token");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Procoding.ApplicationTracker.Domain.Entities.Candidate.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("character varying(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Candidates", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.ComplexProperty<Dictionary<string, object>>("CompanyName", "Procoding.ApplicationTracker.Domain.Entities.Company.CompanyName#CompanyName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("OfficialWebSiteLink", "Procoding.ApplicationTracker.Domain.Entities.Company.OfficialWebSiteLink#Link", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("character varying(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.CompanyAverageGrossSalary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("GrossSalary")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyAverageGrossSalaries", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Procoding.ApplicationTracker.Domain.Entities.Employee.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("character varying(512)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.InterviewStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("InteviewStepType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("JobApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("JobApplicationId");

                    b.ToTable("InterviewSteps", (string)null);
                });

            modelBuilder.Entity("Procoding.ApplicationTracker.Domain.Entities.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationSourceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AppliedOnUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("JobApplicationStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("JobPositionTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.ComplexProperty<Dictionary<string, object>>("JobAdLink", "Procoding.ApplicationTracker.Domain.Entities.JobApplication.JobAdLink#Link", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("character varying(512)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("JobType", "Procoding.ApplicationTracker.Domain.Entities.JobApplication.JobType#JobType", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("WorkLocationType", "Procoding.ApplicationTracker.Domain.Entities.JobApplication.WorkLocationType#WorkLocationType", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("character varying(64)");
                        });

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
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

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
