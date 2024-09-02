using DataAccess.Models;

namespace DataAccess.Data;
public interface IPostData
{
  
    Task<int> InsertPost(PostModel post);

    Task<IEnumerable<PostModel>> GetPosts();
 
}