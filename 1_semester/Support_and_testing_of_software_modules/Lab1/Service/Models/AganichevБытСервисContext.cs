using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class AganichevБытСервисContext : DbContext
{
    public AganichevБытСервисContext()
    {
    }

    public AganichevБытСервисContext(DbContextOptions<AganichevБытСервисContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<RepairPart> RepairParts { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<TechModel> TechModels { get; set; }

    public virtual DbSet<TechType> TechTypes { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DBSRV\\vip2024;Database=Aganichev_БытСервис;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Clients__CB9A1CDF56AF6E4B");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Managers__CB9A1CDFDFF91341");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Masters__CB9A1CDFEDEC8E17");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<RepairPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RepairPa__3214EC276B85CF9F");

            entity.ToTable("RepairPart");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RepairParts)
                .HasMaxLength(255)
                .HasColumnName("repairParts");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__E3C5DE51761399E6");

            entity.Property(e => e.RequestId).HasColumnName("requestID");
            entity.Property(e => e.CompletionDate)
                .HasColumnType("datetime")
                .HasColumnName("completionDate");
            entity.Property(e => e.FkClientId).HasColumnName("FK_clientID");
            entity.Property(e => e.FkRepairPartId).HasColumnName("FK_repairPartID");
            entity.Property(e => e.FkRequestStatusId).HasColumnName("FK_requestStatusID");
            entity.Property(e => e.FkTechModelId).HasColumnName("FK_techModelID");
            entity.Property(e => e.ProblemDescryption)
                .HasMaxLength(255)
                .HasColumnName("problemDescryption");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");

            entity.HasOne(d => d.FkClient).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FkClientId)
                .HasConstraintName("FK__Requests__FK_cli__5DCAEF64");

            entity.HasOne(d => d.FkRepairPart).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FkRepairPartId)
                .HasConstraintName("FK__Requests__FK_rep__5CD6CB2B");

            entity.HasOne(d => d.FkRequestStatus).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FkRequestStatusId)
                .HasConstraintName("FK__Requests__FK_req__5BE2A6F2");

            entity.HasOne(d => d.FkTechModel).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FkTechModelId)
                .HasConstraintName("FK__Requests__FK_tec__5AEE82B9");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__requestS__3214EC27B449D8F5");

            entity.ToTable("requestStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RequestStatus1)
                .HasMaxLength(255)
                .HasColumnName("requestStatus");
        });

        modelBuilder.Entity<TechModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TechMode__3214EC27AAC77E4C");

            entity.ToTable("TechModel");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkTechType).HasColumnName("FK_techType");
            entity.Property(e => e.HomeTechModel)
                .HasMaxLength(255)
                .HasColumnName("homeTechModel");

            entity.HasOne(d => d.FkTechTypeNavigation).WithMany(p => p.TechModels)
                .HasForeignKey(d => d.FkTechType)
                .HasConstraintName("FK__TechModel__FK_te__5812160E");
        });

        modelBuilder.Entity<TechType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TechType__3214EC2707AB6BEE");

            entity.ToTable("TechType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HomeTechType)
                .HasMaxLength(255)
                .HasColumnName("homeTechType");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.WorkId).HasName("PK__Works__F2686A58CDA3A047");

            entity.Property(e => e.WorkId).HasColumnName("workID");
            entity.Property(e => e.FkMasterId).HasColumnName("FK_masterID");
            entity.Property(e => e.FkRequestId).HasColumnName("FK_requestID");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .HasColumnName("message");

            entity.HasOne(d => d.FkMaster).WithMany(p => p.Works)
                .HasForeignKey(d => d.FkMasterId)
                .HasConstraintName("FK__Works__FK_master__5535A963");

            entity.HasOne(d => d.FkRequest).WithMany(p => p.Works)
                .HasForeignKey(d => d.FkRequestId)
                .HasConstraintName("FK__Works__FK_reques__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
