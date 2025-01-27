namespace Blog_Backend.Models;

/// <summary>
/// User posts only model
/// </summary>
/// 
public class UserPostModelResponse
{

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }

}
