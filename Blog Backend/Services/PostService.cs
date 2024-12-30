
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Blog_Backend.Models;
using FirebaseAdmin.Auth;
using System.Security.Claims;

namespace Blog_Backend.Services;

public class PostService : IPostService
{

    private readonly IPostData _postData;
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly IUserData _userData;
    private readonly IS3Service _s3Service;
    private readonly IConfiguration _config;

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
        try
        {

            var thumbnail = "";

            if (post.Image != null)
            {

                var bucketName = _config["AwsConfiguration:BucketName"];
                var AwsAcessKey = _config["AwsConfiguration:AcessKey"];
                var AwsSecretKey = _config["AwsConfiguration:SecretKey"];
                var credentials = new BasicAWSCredentials(AwsAcessKey, AwsSecretKey);

                var config = new AmazonS3Config()
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1
                };

                using var client = new AmazonS3Client(credentials, config);

                var fileName = Path.GetFileName(post.Image.FileName);
                var file = post.Image;

                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var objName = $"{Guid.NewGuid()}.{fileName}";

                await _s3Service.UploadFileAsync(client, bucketName, objName, memoryStream);

                thumbnail = $"https://{bucketName}.s3.amazonaws.com/{objName}";

            }

            post.Thumbnail = thumbnail;

            var userId = _claimsPrincipal.FindFirstValue("Id");

            await _postData.InsertPost(new PostModel { UserId = userId, Title = post.Title, Description = post.Description, Thumbnail = post.Thumbnail });

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

    public async Task<PostModel> GetPostById(int id)
    {

       var post = await _postData.GetPostById(id);

        return post;
    }
}
