using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

public partial class MyGarageFinderDbContext : DbContext
{
    public MyGarageFinderDbContext()
    {
    }

    public MyGarageFinderDbContext(DbContextOptions<MyGarageFinderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<GarageImage> GarageImages { get; set; }

    public virtual DbSet<MessageSender> MessageSenders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleUser> VehicleUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=MyGarageFinderDB;User ID=MyGarageFinderAdminLogin;Password=DAN1706;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2D40C2943");

            entity.HasOne(d => d.Garage).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Garag__403A8C7D");

            entity.HasOne(d => d.Status).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Statu__4222D4EF");

            entity.HasOne(d => d.VehicleUser).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Vehic__412EB0B6");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Appointm__C8EE20436537C74C");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ChatMess__C87C037CFC8FB24A");

            entity.HasOne(d => d.Garage).WithMany(p => p.ChatMessages).HasConstraintName("FK__ChatMessa__Garag__33D4B598");

            entity.HasOne(d => d.MessageSender).WithMany(p => p.ChatMessages).HasConstraintName("FK__ChatMessa__Messa__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.ChatMessages).HasConstraintName("FK__ChatMessa__UserI__32E0915F");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("PK__Garage__5D8BEEB149A7FCE5");
        });

        modelBuilder.Entity<GarageImage>(entity =>
        {
            entity.HasKey(e => e.GarageImageId).HasName("PK__GarageIm__3A316295862260D9");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageImages).HasConstraintName("FK__GarageIma__Garag__3B75D760");
        });

        modelBuilder.Entity<MessageSender>(entity =>
        {
            entity.HasKey(e => e.MessageSenderId).HasName("PK__MessageS__229AE8F14C426610");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79AE7C063331");

            entity.HasOne(d => d.Garage).WithMany(p => p.Reviews).HasConstraintName("FK__Review__GarageID__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Review__UserID__37A5467C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6E7774E3");

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users).HasConstraintName("FK__Users__UserStatu__267ABA7A");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__UserStat__C8EE2043D6A297B0");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.LicensePlate).HasName("PK__Vehicle__026BC15D594B2C7A");

            entity.Property(e => e.LicensePlate).ValueGeneratedNever();
        });

        modelBuilder.Entity<VehicleUser>(entity =>
        {
            entity.HasKey(e => e.VehicleUserId).HasName("PK__VehicleU__622C9174A8CFB827");

            entity.HasOne(d => d.User).WithMany(p => p.VehicleUsers).HasConstraintName("FK__VehicleUs__UserI__2C3393D0");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleUsers).HasConstraintName("FK__VehicleUs__Vehic__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
