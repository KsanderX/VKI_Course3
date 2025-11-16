using Microsoft.EntityFrameworkCore;

namespace Demo_Shoes.Models;

public partial class AganichevShoesContext : DbContext
{
    public AganichevShoesContext()
    {
        Database.EnsureCreated();
    }

    public AganichevShoesContext(DbContextOptions<AganichevShoesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderContent> OrderContents { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PickUpPoint> PickUpPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductName> ProductNames { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer("Server=PC_Sanya;Database=Aganichev_Shoes;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC27C2392E91");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC2767148039");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DeliveryDay).HasColumnType("datetime");
            entity.Property(e => e.FkClient).HasColumnName("FK_Client");
            entity.Property(e => e.FkOrderStatus).HasColumnName("FK_OrderStatus");
            entity.Property(e => e.FkPickUpPoint).HasColumnName("FK_PickUpPoint");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkClient)
                .HasConstraintName("FK__Order__FK_Client__4CA06362");

            entity.HasOne(d => d.FkOrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderStatus)
                .HasConstraintName("FK__Order__FK_OrderS__4D94879B");

            entity.HasOne(d => d.FkPickUpPointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkPickUpPoint)
                .HasConstraintName("FK__Order__FK_PickUp__4E88ABD4");
        });

        modelBuilder.Entity<OrderContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_Co__3214EC275ED496D2");

            entity.ToTable("Order_Contents");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FkOrder).HasColumnName("FK_Order");
            entity.Property(e => e.FkProduct).HasColumnName("FK_Product");

            entity.HasOne(d => d.FkOrderNavigation).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.FkOrder)
                .HasConstraintName("FK__Order_Con__FK_Or__4F7CD00D");

            entity.HasOne(d => d.FkProductNavigation).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.FkProduct)
                .HasConstraintName("FK__Order_Con__FK_Pr__5070F446");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC27CD2AA103");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<PickUpPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PickUpPo__3214EC27C0BCDF04");

            entity.ToTable("PickUpPoint");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Addreses).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC27E63D342E");

            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Article).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FkManufacturer).HasColumnName("FK_Manufacturer");
            entity.Property(e => e.FkProductCategory).HasColumnName("FK_ProductCategory");
            entity.Property(e => e.FkProductName).HasColumnName("FK_ProductName");
            entity.Property(e => e.FkSupplier).HasColumnName("FK_Supplier");
            entity.Property(e => e.FkUnit).HasColumnName("FK_Unit");
            entity.Property(e => e.Photo).HasMaxLength(255);

            entity.HasOne(d => d.FkManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkManufacturer)
                .HasConstraintName("FK__Product__FK_Manu__5165187F");

            entity.HasOne(d => d.FkProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkProductCategory)
                .HasConstraintName("FK__Product__FK_Prod__534D60F1");

            entity.HasOne(d => d.FkProductNameNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkProductName)
                .HasConstraintName("FK__Product__FK_Prod__52593CB8");

            entity.HasOne(d => d.FkSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkSupplier)
                .HasConstraintName("FK__Product__FK_Supp__5441852A");

            entity.HasOne(d => d.FkUnitNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkUnit)
                .HasConstraintName("FK__Product__FK_Unit__5535A963");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC27B2246FD3");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product___3214EC27F518CBF1");

            entity.ToTable("Product_Name");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC275944F338");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC27F0B7CCE6");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unit__3214EC279CDD68A7");

            entity.ToTable("Unit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC278AAE71B4");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("FIO");
            entity.Property(e => e.FkUserRole).HasColumnName("FK_UserRole");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.FkUserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkUserRole)
                .HasConstraintName("FK__User__FK_UserRol__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
