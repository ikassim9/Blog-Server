using Blog_Backend.Models;
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
    public async Task<ActionResult> CreatePost(PostRequest post)
    {

        try
        {
            await _postService.CreatePost(post);

        }
        catch (Exception)
        {

            throw;
        }


        return Ok();
    }

 
    [HttpGet("GetPosts")]
    public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
    {

        try
        {
             var response = await _postService.GetPosts();

            return Ok(response);
        }
        catch (Exception e)
        {

            Console.WriteLine(e);

            throw;
        }

    }
}
