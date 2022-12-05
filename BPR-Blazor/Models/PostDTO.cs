namespace BPR_Blazor.Models
{
    public class PostDTO
    {
        public string Username { get; set; }
        public string Username2 { get; set; }
        public string? Creator { get; set; }
        public int Id { get; set; }
        public int followedId { get; set; }
        public bool isUser { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Reaction { get; set; }
        public int Likes { get; set; }
    }
}
