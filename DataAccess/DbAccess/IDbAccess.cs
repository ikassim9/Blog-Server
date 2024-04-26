
namespace DataAccess.DbAccess;

public interface IDbAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string procedure, U parameters, string connectionId = "Default");
    Task<int> SaveData<T>(string procedure, T parameters, string connectionId = "Default");
}