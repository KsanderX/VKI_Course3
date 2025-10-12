using MyAppToday.Model;

namespace MyAppToday.Service
{
    public interface IAuthService
    {
        public Operator Operator { get; set; }
        public Client Client { get; set; }
        public Master Master { get; set; }
        public object Login(string login, string password);
        public void Logout();
    }
}
