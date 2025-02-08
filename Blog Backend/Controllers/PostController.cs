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
    private readonly ILogger<PostController> _logger;

    public PostController(IPostService postService, IUserData userData, ILogger<PostController> logger)
    {
        _postService = postService;
        _userData = userData;
        _logger = logger;
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


    [HttpGet("GetPostById/{id}")]

    public  async Task<ActionResult<PostModel>> GetPostById(int id)
    {
        var response = await _postService.GetPostById(id);

        return Ok(response);
    }

 
    [HttpGet("GetPostByUserId/{profileId}")]
    public async Task<ActionResult<IEnumerable<PostModel>>> GetPostByUserId(string profileId)
    {
        try
        {
            var response = await _postService.GetPostByUserId(profileId);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return StatusCode(500, "An unexpected error occurred.");

        }

    }

    [Authorize]
    [HttpPut("{postId}")]
    public async Task<ActionResult> UpdatePost(int postId, PostRequest post)
    {
        try
        {
            await _postService.UpdatePost(postId, post);

            return Ok();
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);

        }

        catch (Exception e)
        {
            return StatusCode(500, "An unexpected error occurred.");

        }

    }

    [Authorize]

    [HttpDelete("{postId}")]

    public async Task<ActionResult> DeletePost(int postId)
    {

        try
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }

        catch (Exception e)
        {
            return StatusCode(500, "An unexpected error occurred.");

        }

    }

}
