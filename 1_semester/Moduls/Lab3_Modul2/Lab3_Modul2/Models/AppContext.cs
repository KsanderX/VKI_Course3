

namespace Lab3_Modul2.Models
{
    public static class AppContext
    {
        public static List<Role> Roles { get; set; } = new()
        {
            new Role { ID = 1, Name = "Пользователь" },
            new Role { ID = 2, Name = "Менеджер" },
            new Role { ID = 3, Name = "Администратор" }
        };

        public static List<User> Users { get; set; } = new()
        {
            new User { ID = 1, Name = "1", Login = "1", Password = "1", Role = Roles.FirstOrDefault(r => r.Name == "User") ?? Roles[0] },
            new User { ID = 2, Name = "2", Login = "2", Password = "2", Role = Roles.FirstOrDefault(r => r.Name == "Manadger") ?? Roles[1] },
            new User { ID = 3, Name = "3", Login = "3", Password = "3", Role = Roles.FirstOrDefault(r => r.Name == "Admin") ?? Roles[2] }
        };
    }
}
