using Blog_Backend.Models;
using System.Security.Claims;

namespace Blog_Backend.Services;

public class PostService : IPostService
{

    private readonly IPostData _postData;
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly IUserData _userData;
    private readonly IS3Service _s3Service;
    private readonly IConfiguration _config;
    private readonly string bucketName =  "blogapp-thumbnail";
    public PostService(IPostData postData, ClaimsPrincipal claimsPrincipal, IUserData userData, IS3Service s3, IConfiguration config)
    {

        _postData = postData;
        _claimsPrincipal = claimsPrincipal;
        _userData = userData;
        _s3Service = s3;
        _config = config;
    }
    public async Task CreatePost(PostRequest post)
    {
 
        var thumbnail = "";
        if (post.Image != null)
        {
            var file = post.Image;

            thumbnail = await _s3Service.UploadThumbnailAsync(file, bucketName);
        }

        var userId = _claimsPrincipal.FindFirstValue("Id");

        await _postData.InsertPost(new PostModel { UserId = userId, Title = post.Title, Description = post.Description, Thumbnail = thumbnail });

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

    public async Task<PostModel> GetPostById(int id)
    {

        var post = await _postData.GetPostById(id);

        return post;
    }

    public async Task<IEnumerable<UserPostModelResponse>> GetPostByUserId(string profileId)
    {
        try
        {

            var posts = await _postData.GetPostByUserId(profileId);

            return posts.Select(post => new UserPostModelResponse
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Thumbnail = post.Thumbnail
            });


        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }

    public async Task UpdatePost(int postId, PostRequest post)
    {
        var userId = _claimsPrincipal.FindFirstValue("Id");
 

        var userRecord = await _userData.GetUser(userId);
        var postToEdit = await _postData.GetPostById(postId);

        if (userRecord != null && postToEdit.UserId == userId)
        {

            // user added or change thumnail
            if (post.Image != null)
            {

                // remove thumnail from s3 bucket if it exists
                if (postToEdit.Thumbnail != "" && postToEdit.Thumbnail != null)
                {
                    var objectname = postToEdit.Thumbnail.Split("/").Last();

                    await _s3Service.DeleteThumbnailAsync(bucketName, objectname);

                }

                //adding the new thumnail
                var file = post.Image;

                var thumbnail = await _s3Service.UploadThumbnailAsync(file, bucketName);


                await _postData.UpdatePost(postId, new PostModel { UserId = userId, Title = post.Title, Description = post.Description, Thumbnail = thumbnail });
            }

            // user remove or unmodify thumnail
            else
            {
                // thumnail is removed case
                if (post.IsThumbnailRemoved && postToEdit.Thumbnail != "" && postToEdit.Thumbnail != null)
                {
                    var objectname = postToEdit.Thumbnail.Split("/").Last();


                    await _s3Service.DeleteThumbnailAsync(bucketName, objectname);

                    await _postData.UpdatePost(postId, new PostModel { UserId = userId, Title = post.Title, Description = post.Description, Thumbnail = "" });

                }

                else
                {
                    // in cse thumnail is not change, send existing thumnail data

                    await _postData.UpdatePost(postId, new PostModel { UserId = userId, Title = post.Title, Description = post.Description, Thumbnail = postToEdit.Thumbnail });

                }
            }

        }


        else
        {
            throw new UnauthorizedAccessException("User is not authorize to perform this operation");
        }
       


    }




    public async Task DeletePost(int postId)
    {
        var userId = _claimsPrincipal.FindFirstValue("Id");
 
        if (userId != null)
        {
            var userRecord = await _userData.GetUser(userId);

            if (userRecord != null)
            {
                var postToDelete = await _postData.GetPostById(postId);

                if (userId == postToDelete.UserId)
                {

                    // need to first remove s3 ojbect from bucket before deletign record in db
                    if (postToDelete != null && !string.IsNullOrWhiteSpace(postToDelete.Thumbnail))
                    {

                        
                        var objectname = postToDelete.Thumbnail.Split("/").Last();
 
                        await _s3Service.DeleteThumbnailAsync(bucketName, objectname);


                    }

                    // delete from db
                    await _postData.DeletePost(postId);
                }
            }
            else
            {
                throw new UnauthorizedAccessException("User is not authorize to perform this operation");
            }
        }
    }
}
