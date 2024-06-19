﻿// <auto-generated />
using System;
using Gultan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gultan.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240617065535_AddBlabla")]
    partial class AddBlabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gultan.Domain.Models.Forecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ForecastDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PredictedPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("StockId");

                    b.ToTable("Forecasts");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Goals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Образование"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Покупка недвижимости"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Путешествие"
                        });
                });

            modelBuilder.Entity("Gultan.Domain.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Exchange")
                        .HasColumnType("text");

                    b.Property<string>("Industry")
                        .HasColumnType("text");

                    b.Property<decimal>("LastPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Long")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Middle")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Sector")
                        .HasColumnType("text");

                    b.Property<decimal>("Short")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Gultan.Domain.Models.StockPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("HighPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("OpenPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("StockPrices");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Token", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Gultan.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivationLink")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Surname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Capital")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("GoalId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("RiskLevel")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Gultan.Domain.Models.WalletStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<int>("WalletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("WalletId");

                    b.ToTable("WalletStocks");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Forecast", b =>
                {
                    b.HasOne("Gultan.Domain.Models.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gultan.Domain.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Gultan.Domain.Models.StockPrice", b =>
                {
                    b.HasOne("Gultan.Domain.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Token", b =>
                {
                    b.HasOne("Gultan.Domain.Models.User", "User")
                        .WithOne("Token")
                        .HasForeignKey("Gultan.Domain.Models.Token", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gultan.Domain.Models.Wallet", b =>
                {
                    b.HasOne("Gultan.Domain.Models.Goal", "Goal")
                        .WithMany()
                        .HasForeignKey("GoalId");

                    b.HasOne("Gultan.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gultan.Domain.Models.WalletStock", b =>
                {
                    b.HasOne("Gultan.Domain.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gultan.Domain.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Gultan.Domain.Models.User", b =>
                {
                    b.Navigation("Token")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
