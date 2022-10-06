namespace BPR_API.APIModels
{
    public class PostDTO
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Reaction { get; set; } // 0 - Nothing | 1 - Like | 2 - Dislike
    }
}
