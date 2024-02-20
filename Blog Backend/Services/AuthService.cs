using Blog_Backend.Models;
using FirebaseAdmin.Auth;

namespace Blog_Backend.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> AuthorizeUser(string userToken)
        {
            try
            {

                // extract token from header

                string token = userToken.Split(" ")[1];

                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(token);

                string uid = decodedToken.Uid;

                return uid;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
