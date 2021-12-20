﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Model.AccountActivity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BrowserName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<string>("OsName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountActivities");
                });

            modelBuilder.Entity("Domain.Model.CipherLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EncryptedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CipherLogins");
                });

            modelBuilder.Entity("Domain.Model.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BrowserName")
                        .HasColumnType("text");

                    b.Property<long>("ExpirationUnixTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<string>("OsName")
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshTokenGuid")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Domain.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmailVerificationTokenHash")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EmailVerificationTokenValidTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EncryptionKeyHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IdEmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetTokenHash")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PasswordResetTokenValidTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UniversalToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Model.AccountActivity", b =>
                {
                    b.HasOne("Domain.Model.User", null)
                        .WithMany("AccountActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Model.Session", b =>
                {
                    b.HasOne("Domain.Model.User", null)
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Model.User", b =>
                {
                    b.Navigation("AccountActivities");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
