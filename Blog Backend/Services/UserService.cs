using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using System.Xml.Linq;

namespace Blog_Backend.Services
{
    public class UserService : IUserService
    {


        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly IUserData _userData;

        public UserService(ClaimsPrincipal claimsPrincipal, IUserData userData)
        {
            _claimsPrincipal = claimsPrincipal;
            _userData = userData;
        }

        public async Task<int> RegiserUser()
        {

            try
            {
                var userId = _claimsPrincipal.FindFirstValue("Id");

                var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
 
                return await _userData.InsertUser(new UserModel { Name = userRecord.DisplayName, UserId = userRecord.Uid });

 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
               

            }

        public async Task LoginUser()
        {
            try
            {
                var userId = _claimsPrincipal.FindFirstValue("Id");
                var name = _claimsPrincipal.FindFirstValue("Name");

                var user = await _userData.GetUser(userId);

                // if no user, create user in db
                if (user == null)
                {
                    var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);

                    await _userData.InsertUser(new UserModel { Name = userRecord.DisplayName, UserId = userId });
                }

                
            }
            catch (Exception)
            {

                throw;
            }

           


        }
    }
}
