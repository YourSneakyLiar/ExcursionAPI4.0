﻿// <auto-generated />
using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ExcursionBdContext))]
    [Migration("20250411065520_monno")]
    partial class monno
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.Property<int>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ComplaintID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComplaintId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValueSql("('Open')");

                    b.Property<int?>("TourId")
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ComplaintId")
                        .HasName("PK__Complain__740D89AFF6AEADED");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("Domain.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NotificationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("NotificationId")
                        .HasName("PK__Notifica__20CF2E32C47DD7A4");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValueSql("('Pending')");

                    b.Property<int?>("TourId")
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BAF24D78EA3");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Models.ProviderService", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ServiceID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int")
                        .HasColumnName("ProviderID");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ServiceId")
                        .HasName("PK__Provider__C51BB0EA937992DF");

                    b.HasIndex("ProviderId");

                    b.ToTable("ProviderServices");
                });

            modelBuilder.Entity("Domain.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReviewID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("TourId")
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ReviewId")
                        .HasName("PK__Reviews__74BC79AECA20445D");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Domain.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__8AFACE3A23BED8A5");

                    b.HasIndex(new[] { "RoleName" }, "UQ__Roles__8A2B61607B249946")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Models.Statistic", b =>
                {
                    b.Property<int>("StatisticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StatisticID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatisticId"));

                    b.Property<int?>("TotalBookings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("TotalRevenue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TourId")
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    b.HasKey("StatisticId")
                        .HasName("PK__Statisti__367DEB37FE5B723A");

                    b.HasIndex("TourId");

                    b.ToTable("Statistic", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SubscriptionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("SubscriptionId")
                        .HasName("PK__Subscrip__9A2B24BD0E2D18BF");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Domain.Models.Tour", b =>
                {
                    b.Property<int>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TourId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("GuideId")
                        .HasColumnType("int")
                        .HasColumnName("GuideID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int")
                        .HasColumnName("ProviderID");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("TourId")
                        .HasName("PK__Tours__604CEA10A2F3859E");

                    b.HasIndex("GuideId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Domain.Models.TourLoadStatistic", b =>
                {
                    b.Property<int>("StatisticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StatisticID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatisticId"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("BookedSeats")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("TourId")
                        .HasColumnType("int")
                        .HasColumnName("TourID");

                    b.HasKey("StatisticId")
                        .HasName("PK__TourLoad__367DEB37D8C7331B");

                    b.HasIndex("TourId");

                    b.ToTable("TourLoadStatistics");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCACE5825CE3");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Username" }, "UQ__Users__536C85E45B6333EB")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D10534C5DA5B05")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Models.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.HasOne("Domain.Models.Tour", "Tour")
                        .WithMany("Complaints")
                        .HasForeignKey("TourId")
                        .HasConstraintName("FK__Complaint__TourI__534D60F1");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Complaint__UserI__52593CB8");

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Notification", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Notificat__UserI__5812160E");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.HasOne("Domain.Models.Tour", "Tour")
                        .WithMany("Orders")
                        .HasForeignKey("TourId")
                        .HasConstraintName("FK__Orders__TourID__47DBAE45");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Orders__UserID__46E78A0C");

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.ProviderService", b =>
                {
                    b.HasOne("Domain.Models.User", "Provider")
                        .WithMany("ProviderServices")
                        .HasForeignKey("ProviderId")
                        .HasConstraintName("FK__ProviderS__Provi__74AE54BC");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Domain.Models.Review", b =>
                {
                    b.HasOne("Domain.Models.Tour", "Tour")
                        .WithMany("Reviews")
                        .HasForeignKey("TourId")
                        .HasConstraintName("FK__Reviews__TourID__4D94879B");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Reviews__UserID__4CA06362");

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Statistic", b =>
                {
                    b.HasOne("Domain.Models.Tour", "Tour")
                        .WithMany("Statistics")
                        .HasForeignKey("TourId")
                        .HasConstraintName("FK__Statistic__TourI__5CD6CB2B");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Domain.Models.Subscription", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Subscript__UserI__6D0D32F4");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Tour", b =>
                {
                    b.HasOne("Domain.Models.User", "Guide")
                        .WithMany("TourGuides")
                        .HasForeignKey("GuideId")
                        .HasConstraintName("FK__Tours__GuideID__4222D4EF");

                    b.HasOne("Domain.Models.User", "Provider")
                        .WithMany("TourProviders")
                        .HasForeignKey("ProviderId")
                        .HasConstraintName("FK__Tours__ProviderI__412EB0B6");

                    b.Navigation("Guide");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Domain.Models.TourLoadStatistic", b =>
                {
                    b.HasOne("Domain.Models.Tour", "Tour")
                        .WithMany("TourLoadStatistics")
                        .HasForeignKey("TourId")
                        .HasConstraintName("FK__TourLoadS__TourI__70DDC3D8");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__Users__RoleID__3D5E1FD2");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.Tour", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Statistics");

                    b.Navigation("TourLoadStatistics");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Notifications");

                    b.Navigation("Orders");

                    b.Navigation("ProviderServices");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reviews");

                    b.Navigation("Subscriptions");

                    b.Navigation("TourGuides");

                    b.Navigation("TourProviders");
                });
#pragma warning restore 612, 618
        }
    }
}
