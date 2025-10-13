using Microsoft.EntityFrameworkCore;

namespace MyAppToday.Model
{
    public class MyDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Equipment> Equipment {  get; set; }
        public DbSet<RequestMasters> RequestMasters { get; set; }
        public MyDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PC_Sanya;Initial Catalog=NewDbCheck;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name="Sanya", Login = "Sanya", Password = "qwer" },
                new Client { Id = 2, Name="Nina", Login = "Nina", Password = "123" },
                new Client { Id = 3, Name="Petr", Login = "Petr", Password = "456" }
                );

            modelBuilder.Entity<Master>().HasData(
               new Master { Id = 1, Name="Alex", Login = "Alex", Password = "zxcv" },
               new Master { Id = 2, Name="Oleg", Login = "Oleg", Password = "312" }
               );

            modelBuilder.Entity<Operator>().HasData(
              new Operator { Id = 1, Name="Nastya", Login = "Nastya1", Password = "anst" }
              );

            modelBuilder.Entity<Equipment>().HasData(
             new Equipment { Id = 1, Name = "Стиральная машина" },
             new Equipment { Id = 2, Name = "Сотовый телефон" },
             new Equipment { Id = 3, Name = "Принтер"},
             new Equipment { Id = 4, Name = "Ноутбук"}
             );
        }
    }
}
