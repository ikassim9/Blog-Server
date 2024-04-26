using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class UserData : IUserData
{

    private readonly IDbAccess _db;

    public UserData(IDbAccess db)
    {
        _db = db;
    }


    public Task<IEnumerable<UserModel>> GetUsers()
    {

        return _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    }

    public async Task<UserModel?> GetUser(string id)
    {

        var results = await _db.LoadData<UserModel, dynamic>
             ("dbo.spUser_Get", new { Id = id });


        return results.FirstOrDefault();
    }

    public Task<int> InsertUser(UserModel user)
    {
       return _db.SaveData<dynamic>("dbo.spUser_Insert", new { user.Name, user.UserId });

    }

    public Task<int> UpdateUser(UserModel user)
    {

        return _db.SaveData("dbo.spUser_Update", user);

    }
    public Task<int> DeleteUser(int id)
    {
        return _db.SaveData("dbo.spUser_Delete", new { Id = id });

    }
}
