using AMSWebApi.Models;
using AMSWebApi.Repository;

namespace AMSWebApi.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginService> _logger;

        public LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger)
        {
            _loginRepository = loginRepository;
            _logger = logger;
        }

        public User AuthenticateUser(string username, string password)
        {
            var user = _loginRepository.ValidateUser(username, password);

            if (user == null)
            {
                _logger.LogWarning($"Authentication failed for user: {username}. Invalid username");
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            //logging in 

            _logger.LogInformation($"User {username} successfully authenticated.");
            return user;
        }
    }
}
