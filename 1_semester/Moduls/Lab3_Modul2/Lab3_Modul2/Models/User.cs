namespace Lab3_Modul2.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return $"{ID} - {Role.Name}";
        }
    }
}
