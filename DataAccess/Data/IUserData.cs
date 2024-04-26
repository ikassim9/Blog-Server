using DataAccess.Models;

namespace DataAccess.Data;
public interface IUserData
{
    Task<int> DeleteUser(int id);
    Task<UserModel?> GetUser(string id);
    Task<IEnumerable<UserModel>> GetUsers();
    Task<int> InsertUser(UserModel user);
    Task<int> UpdateUser(UserModel user);
}