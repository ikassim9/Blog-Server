namespace Blog_Backend.Services;

public interface IUserService
{
    Task<int> RegiserUser();
    Task LoginUser();
}