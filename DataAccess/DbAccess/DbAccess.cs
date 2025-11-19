using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace DataAccess.DbAccess;
public class DbAccess : IDbAccess
{
    private readonly IConfiguration _config;

    public DbAccess(IConfiguration config)
    {
        _config = config;
    }



    public async Task<IEnumerable<T>> LoadData<T, U>(
        string procedure,
        U parameters,
        string connectionId = "Default")
    {

        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(procedure, parameters,
            commandType: CommandType.StoredProcedure);
    }


    public async Task<int> SaveData<T>(

       string procedure,
       T parameters,
       string connectionId = "Default")
    {

        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));


        return await connection.ExecuteAsync(procedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

}