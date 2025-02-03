using DataAccess.Models;

namespace DataAccess.Data;
public interface IPostData
{
  
    Task<int> InsertPost(PostModel post);

    Task<IEnumerable<PostModel>> GetPosts();

    Task<PostModel> GetPostById(int id);
    Task<IEnumerable<PostModel>> GetPostByUserId(string id);
    Task UpdatePost(int postId, PostModel post);
}