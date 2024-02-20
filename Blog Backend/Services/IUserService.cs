using Blog_Backend.Model;

namespace Blog_Backend.Services
{
    public interface IUserService
    {

          Task CreateUser(User user);
    }
}
