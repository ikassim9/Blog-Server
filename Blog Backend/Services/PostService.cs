
using FirebaseAdmin.Auth;
using System.Security.Claims;

namespace Blog_Backend.Services;

public class PostService : IPostService
{

    private readonly IPostData _postData;
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly IUserData _userData;

    public PostService(IPostData postData, ClaimsPrincipal claimsPrincipal, IUserData userData)
    {

        _postData = postData;
        _claimsPrincipal = claimsPrincipal;
        _userData = userData;
    }
    public async Task CreatePost(string title, string description)
    {
        try
        {
            var userId = _claimsPrincipal.FindFirstValue("Id");

            await _postData.InsertPost(new PostModel { UserId = userId, Title = title, Description = description });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<IEnumerable<PostModel>> GetPosts()
    {
        try
        {
         IEnumerable<PostModel> posts = await _postData.GetPosts();

            return posts.ToList();
 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }

      
    }
}
