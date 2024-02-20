using Blog_Backend.Model;

namespace Blog_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;


        public UserService(IAuthService authService)
        {
            _authService = authService;
        }

        public Task CreateUser(User user)
        {
            try
            {
               


            }
            catch (Exception)
            {

                throw;
            }

            return null;

        }
    }
}
