namespace Blog_Backend.Models;

public class PostRequest
{

    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public IFormFile? Image { get; set; }

    public bool IsThumbnailRemoved { get; set; }
}
