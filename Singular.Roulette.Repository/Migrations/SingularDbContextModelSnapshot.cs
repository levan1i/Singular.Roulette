﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Singular.Roulette.Repository;

#nullable disable

namespace Singular.Roulette.Repository.Migrations
{
    [DbContext(typeof(SingularDbContext))]
    partial class SingularDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Ballance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.AccountType", b =>
                {
                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("TypeId");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Bet", b =>
                {
                    b.Property<long>("BetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("BetAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("BetStringJson")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<long?>("SpinId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserIpAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("WonAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("isFinnished")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("BetId");

                    b.HasIndex("SpinId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.HeartBeet", b =>
                {
                    b.Property<string>("SessionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("SessionId");

                    b.ToTable("HeartBeets");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Spin", b =>
                {
                    b.Property<long>("SpinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WiningNumber")
                        .HasColumnType("int");

                    b.HasKey("SpinId");

                    b.ToTable("Spins");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Transaction", b =>
                {
                    b.Property<long>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("FromAccountId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentTransactionId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TransactionStatusCode")
                        .HasColumnType("int");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("TransactionStatusCode");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.TransactionStatus", b =>
                {
                    b.Property<int>("TransactionStatusCode")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("isFailled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isFinnished")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("TransactionStatusCode");

                    b.ToTable("TransactionStatuses");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.TransactionType", b =>
                {
                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("TransactionTypeId");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Account", b =>
                {
                    b.HasOne("Singular.Roulette.Domain.Models.AccountType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Singular.Roulette.Domain.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Bet", b =>
                {
                    b.HasOne("Singular.Roulette.Domain.Models.Spin", "Spin")
                        .WithMany()
                        .HasForeignKey("SpinId");

                    b.Navigation("Spin");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Singular.Roulette.Domain.Models.TransactionStatus", "TransactionStatus")
                        .WithMany()
                        .HasForeignKey("TransactionStatusCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Singular.Roulette.Domain.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionStatus");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Singular.Roulette.Domain.Models.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
