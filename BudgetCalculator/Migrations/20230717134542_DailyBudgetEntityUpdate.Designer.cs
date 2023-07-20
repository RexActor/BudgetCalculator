﻿// <auto-generated />
using System;
using BudgetCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetCalculator.Migrations;

    [DbContext(typeof(AppDBContext))]
    [Migration("20230717134542_DailyBudgetEntityUpdate")]
    partial class DailyBudgetEntityUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("BudgetCalculator.Models.BudgetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgencyProductiveHours")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cases")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CostCenterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectProductiveHours")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CostCenterId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetCalculator.Models.CostCenterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("CostCenters");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DailyBudget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("DailyAllowedHours")
                        .HasColumnType("REAL");

                    b.Property<int>("DailyCases")
                        .HasColumnType("INTEGER");

                    b.Property<double>("DailyTotalMinutes")
                        .HasColumnType("REAL");

                    b.Property<double>("MonthlyMinutesPerCase")
                        .HasColumnType("REAL");

                    b.Property<int>("WeeklyBudgetsId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("budgetDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("WeeklyBudgetsId");

                    b.ToTable("DailyBudgets");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DailyRolesData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DailyBudgetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DailyHeadCountOfRole")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DailyHoursOfRole")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DailyBudgetId");

                    b.ToTable("DailyRolesData");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DepartmentRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CostCenterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CostCenterId");

                    b.ToTable("DepartmentRoles");
                });

            modelBuilder.Entity("BudgetCalculator.Models.WeeklyBudget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgencyProductiveHours")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cases")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CostCenterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectProductiveHours")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MonthName")
                        .HasColumnType("TEXT");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CostCenterId");

                    b.ToTable("WeeklyBudgets");
                });

            modelBuilder.Entity("BudgetCalculator.Models.BudgetEntity", b =>
                {
                    b.HasOne("BudgetCalculator.Models.CostCenterEntity", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CostCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CostCenter");
                });

            modelBuilder.Entity("BudgetCalculator.Models.CostCenterEntity", b =>
                {
                    b.HasOne("BudgetCalculator.Models.DepartmentEntity", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DailyBudget", b =>
                {
                    b.HasOne("BudgetCalculator.Models.WeeklyBudget", "WeeklyBudgets")
                        .WithMany()
                        .HasForeignKey("WeeklyBudgetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeeklyBudgets");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DailyRolesData", b =>
                {
                    b.HasOne("BudgetCalculator.Models.DailyBudget", null)
                        .WithMany("DailyRoles")
                        .HasForeignKey("DailyBudgetId");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DepartmentRoleEntity", b =>
                {
                    b.HasOne("BudgetCalculator.Models.CostCenterEntity", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CostCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CostCenter");
                });

            modelBuilder.Entity("BudgetCalculator.Models.WeeklyBudget", b =>
                {
                    b.HasOne("BudgetCalculator.Models.BudgetEntity", "Budget")
                        .WithMany()
                        .HasForeignKey("BudgetId");

                    b.HasOne("BudgetCalculator.Models.CostCenterEntity", "CostCenter")
                        .WithMany()
                        .HasForeignKey("CostCenterId");

                    b.Navigation("Budget");

                    b.Navigation("CostCenter");
                });

            modelBuilder.Entity("BudgetCalculator.Models.DailyBudget", b =>
                {
                    b.Navigation("DailyRoles");
                });
#pragma warning restore 612, 618
        }
    }
