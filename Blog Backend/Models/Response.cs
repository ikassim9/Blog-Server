namespace Blog_Backend.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Data { get; set; }
        public string ErrorMessage { get; set; }
         
    }
}