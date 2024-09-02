namespace Blog_Backend.Services;

public interface IPostService
{
    Task CreatePost(string title, string description);

    Task<IEnumerable<PostModel>> GetPosts();
}
