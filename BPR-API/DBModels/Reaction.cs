namespace BPR_API.DBModels
{
    public class Reaction
    {
        public int PostId { get; set; }
        public int UserId { get; set; } 
        public int ReactionNumber { get; set; } // 1 - Like | 2 - Dislike
    }
}
