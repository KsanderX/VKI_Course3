using AI_Demo.Models;

namespace AI_Demo.Classes
{
    public static class Helper
    {
        private static ShoesContext _context;
        public static ShoesContext GetContext()
        {
            if (_context == null)
                _context = new ShoesContext();
            return _context;
        }

        // Хранение авторизованного пользователя
        // User - это класс, сгенерированный скаффолдингом
        public static User User;
    }
}
