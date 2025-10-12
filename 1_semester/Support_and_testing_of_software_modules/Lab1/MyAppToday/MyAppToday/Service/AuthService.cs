using MyAppToday.Model;

namespace MyAppToday.Service
{
    public class AuthService : IAuthService
    {
        private MyDbContext _context;
        public Client Client { get; set; }
        public Master Master { get; set; }
        public Operator Operator { get; set; }
        public AuthService()
        {
            _context = new MyDbContext();
        }
        public object Login(string login, string password)
        {
            Client client = _context.Clients.Where(c => c.Login == login && c.Password == password).FirstOrDefault();
            Master master = _context.Masters.Where(m => m.Login == login && m.Password == password).FirstOrDefault();
            Operator oper = _context.Operators.Where(p => p.Login == login && p.Password == password).FirstOrDefault();

            if (client != null)
            {
                Client = client;
                return Client;
            }

            if (master != null)
            {
                Master = master;
                return Master;
            }

            if (oper != null)
            {
                Operator = oper;
                return Operator;
            }

            return null;
        }

        public void Logout()
        {
            Client = null;
            Master = null;
            Operator = null;
        }
    }
}
