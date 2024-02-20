namespace Blog_Backend.Services
{
    public interface IAuthService
    {


        Task<string> AuthorizeUser(string userToken);
    }
}
