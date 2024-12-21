namespace Blog_Backend.Models;

public class PostRequest
{

    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public string? Thumbnail { get; set; }

    public IFormFile? Image { get; set; }
}
