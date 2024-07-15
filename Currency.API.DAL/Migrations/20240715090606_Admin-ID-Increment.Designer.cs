﻿// <auto-generated />
using System;
using Currency.API.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    [DbContext(typeof(CurrencyAPIContext))]
    [Migration("20240715090606_Admin-ID-Increment")]
    partial class AdminIDIncrement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Currency.API.Models.AccountTypeModelAPI", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"));

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AccountID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("UserID");

                    b.ToTable("AccountType");
                });

            modelBuilder.Entity("Currency.API.Models.AdminsModelAPI", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsValidEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Currency.API.Models.BlockedTransactionsModelAPI", b =>
                {
                    b.Property<int>("BlockedTransactionsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlockedTransactionsID"));

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("AdminID")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BlockedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UnBlockedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BlockedTransactionsID");

                    b.HasIndex("AdminID");

                    b.HasIndex("AccountID", "UserID", "CurrencyID", "TimeSent", "Amount");

                    b.ToTable("BlockedTransactions");
                });

            modelBuilder.Entity("Currency.API.Models.BlockedUserLogModelAPI", b =>
                {
                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("AdminID")
                        .HasColumnType("int");

                    b.Property<DateTime>("BlockDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UnblockDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountID", "UserID", "AdminID", "BlockDate");

                    b.HasIndex("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("BlockedUserLog");
                });

            modelBuilder.Entity("Currency.API.Models.CurrencyTypeModelAPI", b =>
                {
                    b.Property<int>("CurrencyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyID"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EUR_Exchange")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GBP_Exchange")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("USD_Exchange")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CurrencyID");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Currency.API.Models.TransactionLogModelAPI", b =>
                {
                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AccountID", "CurrencyID", "UserID", "TimeSent", "Amount");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("UserID");

                    b.ToTable("TransactionLog");
                });

            modelBuilder.Entity("Currency.API.Models.UsersModelAPI", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Currency.API.Models.AccountTypeModelAPI", b =>
                {
                    b.HasOne("Currency.API.Models.CurrencyTypeModelAPI", "CurrencyType")
                        .WithMany("accountTypeModelAPI")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.UsersModelAPI", "Users")
                        .WithMany("accountTypeModelAPI")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyType");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Currency.API.Models.BlockedTransactionsModelAPI", b =>
                {
                    b.HasOne("Currency.API.Models.AdminsModelAPI", "Admins")
                        .WithMany("blockedTransactionsAPI")
                        .HasForeignKey("AdminID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.TransactionLogModelAPI", "TransactionLog")
                        .WithMany("BlockedTransactions")
                        .HasForeignKey("AccountID", "UserID", "CurrencyID", "TimeSent", "Amount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admins");

                    b.Navigation("TransactionLog");
                });

            modelBuilder.Entity("Currency.API.Models.BlockedUserLogModelAPI", b =>
                {
                    b.HasOne("Currency.API.Models.AccountTypeModelAPI", "AccountType")
                        .WithMany("blockedUserLogsAPI")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.AdminsModelAPI", "Admins")
                        .WithMany("blockedUserLogsAPI")
                        .HasForeignKey("AdminID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.UsersModelAPI", "Users")
                        .WithMany("blockedUserLogModelAPI")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");

                    b.Navigation("Admins");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Currency.API.Models.TransactionLogModelAPI", b =>
                {
                    b.HasOne("Currency.API.Models.AccountTypeModelAPI", "AccountType")
                        .WithMany("transactionLogModelAPIs")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.CurrencyTypeModelAPI", "CurrencyType")
                        .WithMany("transactionLogModelAPIs")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Currency.API.Models.UsersModelAPI", "Users")
                        .WithMany("transactionLogModelAPIs")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");

                    b.Navigation("CurrencyType");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Currency.API.Models.AccountTypeModelAPI", b =>
                {
                    b.Navigation("blockedUserLogsAPI");

                    b.Navigation("transactionLogModelAPIs");
                });

            modelBuilder.Entity("Currency.API.Models.AdminsModelAPI", b =>
                {
                    b.Navigation("blockedTransactionsAPI");

                    b.Navigation("blockedUserLogsAPI");
                });

            modelBuilder.Entity("Currency.API.Models.CurrencyTypeModelAPI", b =>
                {
                    b.Navigation("accountTypeModelAPI");

                    b.Navigation("transactionLogModelAPIs");
                });

            modelBuilder.Entity("Currency.API.Models.TransactionLogModelAPI", b =>
                {
                    b.Navigation("BlockedTransactions");
                });

            modelBuilder.Entity("Currency.API.Models.UsersModelAPI", b =>
                {
                    b.Navigation("accountTypeModelAPI");

                    b.Navigation("blockedUserLogModelAPI");

                    b.Navigation("transactionLogModelAPIs");
                });
#pragma warning restore 612, 618
        }
    }
}
