using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class ExcursionBdContext : DbContext
{
    public ExcursionBdContext()
    {
    }

    public ExcursionBdContext(DbContextOptions<ExcursionBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<ProviderService> ProviderServices { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourLoadStatistic> TourLoadStatistics { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-886FNGF4;Database=ExcursionBD;User Id=sa;Password=Password123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Complain__740D89AFF6AEADED");

            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Open')");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Complaint__TourI__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Complaint__UserI__52593CB8");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32C47DD7A4");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__5812160E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF24D78EA3");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Pending')");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Orders__TourID__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__46E78A0C");
        });

        modelBuilder.Entity<ProviderService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Provider__C51BB0EA937992DF");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProviderId).HasColumnName("ProviderID");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Provider).WithMany(p => p.ProviderServices)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__ProviderS__Provi__74AE54BC");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AECA20445D");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Reviews__TourID__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__4CA06362");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A23BED8A5");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61607B249946").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.StatisticId).HasName("PK__Statisti__367DEB37FE5B723A");

            entity.ToTable("Statistic");

            entity.Property(e => e.StatisticId).HasColumnName("StatisticID");
            entity.Property(e => e.TotalBookings).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalRevenue)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Statistic__TourI__5CD6CB2B");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B24BD0E2D18BF");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Subscript__UserI__6D0D32F4");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("PK__Tours__604CEA10A2F3859E");

            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProviderId).HasColumnName("ProviderID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Guide).WithMany(p => p.TourGuides)
                .HasForeignKey(d => d.GuideId)
                .HasConstraintName("FK__Tours__GuideID__4222D4EF");

            entity.HasOne(d => d.Provider).WithMany(p => p.TourProviders)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__Tours__ProviderI__412EB0B6");
        });

        modelBuilder.Entity<TourLoadStatistic>(entity =>
        {
            entity.HasKey(e => e.StatisticId).HasName("PK__TourLoad__367DEB37D8C7331B");

            entity.Property(e => e.StatisticId).HasColumnName("StatisticID");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourLoadStatistics)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__TourLoadS__TourI__70DDC3D8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE5825CE3");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E45B6333EB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C5DA5B05").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
