using AMSWebApi.Models;

namespace AMSWebApi.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AssetsDbContext _context;

        public LoginRepository(AssetsDbContext context)
        {
            _context = context;
        }
        public User ValidateUser(string username, string password)
        {
            if (_context != null)
            {
                User? dbUser = _context.Users.FirstOrDefault(
                    u => u.Username == username && u.Password == password);

                if (dbUser != null)
                {
                    return dbUser;
                }
            }
            return null;
        }
    }
}
