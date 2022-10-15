using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Creator { get; set; }
        public string FollowedId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
