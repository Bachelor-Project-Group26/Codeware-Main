using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public int FollowedId { get; set; }
        [Required]
        public bool IsUser { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int Likes { get; set; }
    }
}
