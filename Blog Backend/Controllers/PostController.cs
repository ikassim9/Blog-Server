using Blog_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IUserData _userData;

    public PostController(IPostService postService, IUserData userData)
    {
        _postService = postService;
        _userData = userData;
    }

    [Authorize]
    [HttpPost("CreatePost")]
    public ActionResult CreatePost(string title, string description)
    {

        _postService.CreatePost(title, description);

        return Ok();
    }

 
    [HttpGet("GetPosts")]
    public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
    {

        try
        {
            //  var response = await _postService.GetPosts();

            var tempData = new List<PostModel>
            {

                new PostModel
                {
                    Title="Apple watch",
                    Id=1,
                    Description="Some description",
                    UserId="1"
                },
                  new PostModel
                {
                    Title="Tesla",
                    Id=2,
                    Description="Some description",
                    UserId="2"
                },
                    new PostModel
                {
                    Title="Television",
                    Id=3,
                    Description="Some description",
                    UserId="3"
                }

            };

            return Ok(tempData);
        }
        catch (Exception e)
        {

            Console.WriteLine(e);

            throw;
        }


     
    }
}
