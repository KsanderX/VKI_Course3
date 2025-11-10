using Microsoft.EntityFrameworkCore;

namespace Lab2_Module2.Models;

public partial class AganichevMusicEquipmentRentalContext : DbContext
{
    public AganichevMusicEquipmentRentalContext()
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public AganichevMusicEquipmentRentalContext(DbContextOptions<AganichevMusicEquipmentRentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderEquipment> OrderEquipments { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PickUpPoint> PickUpPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //=> optionsBuilder.UseSqlServer("Server=DBSRV\\vip2024;Database=Aganichev_MusicEquipmentRental;TrustServerCertificate=True;Integrated Security=True;");
      => optionsBuilder.UseSqlServer("Server=PC_SANYA;Database=1Aganichev_MusicEquipmentRental;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Equipmen__3214EC278342B4D8");

            entity.ToTable("EquipmentType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC275437FCDA");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC27CB73D283");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateRent).HasColumnType("datetime");
            entity.Property(e => e.FkAddressPickUpPoint).HasColumnName("FK_AddressPickUpPoint");
            entity.Property(e => e.FkOrderStatus).HasColumnName("FK_OrderStatus");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkAddressPickUpPointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkAddressPickUpPoint)
                .HasConstraintName("FK__Order__FK_Addres__29221CFB");

            entity.HasOne(d => d.FkOrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderStatus)
                .HasConstraintName("FK__Order__FK_OrderS__2A164134");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkUser)
                .HasConstraintName("FK__Order__FK_User__3A4CA8FD");
        });

        modelBuilder.Entity<OrderEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_Eq__3214EC27BF0329CA");

            entity.ToTable("Order_Equipment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FkEquipment).HasColumnName("FK_Equipment");
            entity.Property(e => e.FkOrder).HasColumnName("FK_Order");

            entity.HasOne(d => d.FkEquipmentNavigation).WithMany(p => p.OrderEquipments)
                .HasForeignKey(d => d.FkEquipment)
                .HasConstraintName("FK__Order_Equ__FK_Eq__4A8310C6");

            entity.HasOne(d => d.FkOrderNavigation).WithMany(p => p.OrderEquipments)
                .HasForeignKey(d => d.FkOrder)
                .HasConstraintName("FK__Order_Equ__FK_Or__41EDCAC5");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_St__3214EC271C6C7FA9");

            entity.ToTable("Order_Status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<PickUpPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PickUpPo__3214EC2719AEFA49");

            entity.ToTable("PickUpPoint");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC27FA06EA82");

            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Article).HasMaxLength(255);
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(255)
                .HasColumnName("Equipment_Name");
            entity.Property(e => e.FkEquipmentType).HasColumnName("FK_Equipment_Type");
            entity.Property(e => e.FkManufacturer).HasColumnName("FK_Manufacturer");
            entity.Property(e => e.FkSupplier).HasColumnName("FK_Supplier");
            entity.Property(e => e.NumberUnits).HasColumnName("Number_Units");
            entity.Property(e => e.Photo).HasMaxLength(255);
            entity.Property(e => e.RentalUnit)
                .HasMaxLength(255)
                .HasColumnName("Rental unit");

            entity.HasOne(d => d.FkEquipmentTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkEquipmentType)
                .HasConstraintName("FK__Product__FK_Equi__4D5F7D71");

            entity.HasOne(d => d.FkManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkManufacturer)
                .HasConstraintName("FK__Product__FK_Manu__4C6B5938");

            entity.HasOne(d => d.FkSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkSupplier)
                .HasConstraintName("FK__Product__FK_Supp__4B7734FF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27A3D77DD9");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC27830512CC");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC2784DE61D7");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("FIO");
            entity.Property(e => e.FkRole).HasColumnName("FK_Role");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRole)
                .HasConstraintName("FK__User__FK_Role__6754599E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
