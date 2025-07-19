using AMSWebApi.Models;

namespace AMSWebApi.Service
{
    public interface ILoginService
    {
        public User AuthenticateUser(string username, string password);
    }
}
