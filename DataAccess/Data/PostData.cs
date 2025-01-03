﻿using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class PostData : IPostData
{


    private readonly IDbAccess _db;

    public PostData(IDbAccess db)
    {
        _db = db;
    }

    public Task<int> InsertPost(PostModel post)
    {
        return _db.SaveData<dynamic>("dbo.spPost_Insert", new { post.UserId, post.Title, post.Description, post.Thumbnail });
    }

    public Task<IEnumerable<PostModel>> GetPosts()
    {
          return _db.LoadData<PostModel, dynamic>("dbo.spPost_GetAll", new { });
    }

    public async Task<PostModel> GetPostById(int id)
    {
        var post = await _db.LoadData<PostModel, dynamic>("dbo.spPost_Get", new { id = id });


        return post.FirstOrDefault();

    }
}
