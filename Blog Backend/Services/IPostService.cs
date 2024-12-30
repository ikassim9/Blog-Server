using Blog_Backend.Models;

namespace Blog_Backend.Services;

public interface IPostService
{
    Task CreatePost(PostRequest post);

    Task<IEnumerable<PostModel>> GetPosts();

    Task<PostModel> GetPostById(int id);
}
