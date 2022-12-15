namespace BPR_Blazor.Models
{
    public class CommentDTO
    {
        
        public int CommentId { get; set; }
       
        public string Username { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public int PostId { get; set; }
       
        public int UserId { get; set; }
        
    }
}