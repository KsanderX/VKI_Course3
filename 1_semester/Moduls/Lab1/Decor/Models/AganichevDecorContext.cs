using Microsoft.EntityFrameworkCore;

namespace Decor.Models;

public partial class AganichevDecorContext : DbContext
{
    public AganichevDecorContext()
    {
    }

    public AganichevDecorContext(DbContextOptions<AganichevDecorContext> options) : base(options)
    {
    }

    public virtual DbSet<MaterialTypeImport> MaterialTypeImports { get; set; }

    public virtual DbSet<MaterialsImport> MaterialsImports { get; set; }

    public virtual DbSet<ProductMaterialsImport> ProductMaterialsImports { get; set; }

    public virtual DbSet<ProductTypeImport> ProductTypeImports { get; set; }

    public virtual DbSet<ProductsImport> ProductsImports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server=DBSRV\\vip2024;Database=Aganichev_Decor;TrustServerCertificate=True;Integrated Security=True;");
        => optionsBuilder.UseSqlServer("Server=PC_Sanya;Database=Aganichev_Decor;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialTypeImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27B0DD5738");

            entity.ToTable("Material_type_import$");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ПроцентБракаМатериала).HasColumnName("Процент брака материала ");
            entity.Property(e => e.ТипМатериала)
                .HasMaxLength(255)
                .HasColumnName("Тип материала");
        });

        modelBuilder.Entity<MaterialsImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27CA74B04B");

            entity.ToTable("Materials_import$");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkТипМатериала).HasColumnName("FK_Тип материала");
            entity.Property(e => e.ЕдиницаИзмерения)
                .HasMaxLength(255)
                .HasColumnName("Единица измерения");
            entity.Property(e => e.КоличествоВУпаковке).HasColumnName("Количество в упаковке");
            entity.Property(e => e.КоличествоНаСкладе).HasColumnName("Количество на складе");
            entity.Property(e => e.МинимальноеКоличество).HasColumnName("Минимальное количество");
            entity.Property(e => e.НаименованиеМатериала)
                .HasMaxLength(255)
                .HasColumnName("Наименование материала");
            entity.Property(e => e.ЦенаЕдиницыМатериала).HasColumnName("Цена единицы материала");

            entity.HasOne(d => d.FkТипМатериалаNavigation).WithMany(p => p.MaterialsImports)
                .HasForeignKey(d => d.FkТипМатериала)
                .HasConstraintName("FK__Materials__FK_Ти__534D60F1");
        });

        modelBuilder.Entity<ProductMaterialsImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product___3214EC275994037E");

            entity.ToTable("Product_materials_import$");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkНаименованиеМатериала).HasColumnName("FK_Наименование материала");
            entity.Property(e => e.FkПродукция).HasColumnName("FK_Продукция");
            entity.Property(e => e.НеобходимоеКоличествоМатериала).HasColumnName("Необходимое количество материала");

            entity.HasOne(d => d.FkНаименованиеМатериалаNavigation).WithMany(p => p.ProductMaterialsImports)
                .HasForeignKey(d => d.FkНаименованиеМатериала)
                .HasConstraintName("FK__Product_m__FK_На__5629CD9C");

            entity.HasOne(d => d.FkПродукцияNavigation).WithMany(p => p.ProductMaterialsImports)
                .HasForeignKey(d => d.FkПродукция)
                .HasConstraintName("FK__Product_m__FK_Пр__5535A963");
        });

        modelBuilder.Entity<ProductTypeImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product___3214EC273CAB7B06");

            entity.ToTable("Product_type_import$");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.КоэффициентТипаПродукции).HasColumnName("Коэффициент типа продукции");
            entity.Property(e => e.ТипПродукции)
                .HasMaxLength(255)
                .HasColumnName("Тип продукции");
        });

        modelBuilder.Entity<ProductsImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC277313DD73");

            entity.ToTable("Products_import$");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkТипПродукции).HasColumnName("FK_Тип продукции");
            entity.Property(e => e.МинимальнаяСтоимостьДляПартнера).HasColumnName("Минимальная стоимость для партнера");
            entity.Property(e => e.НаименованиеПродукции)
                .HasMaxLength(255)
                .HasColumnName("Наименование продукции");
            entity.Property(e => e.ШиринаРулонаМ).HasColumnName("Ширина рулона, м");

            entity.HasOne(d => d.FkТипПродукцииNavigation).WithMany(p => p.ProductsImports)
                .HasForeignKey(d => d.FkТипПродукции)
                .HasConstraintName("FK__Products___FK_Ти__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
