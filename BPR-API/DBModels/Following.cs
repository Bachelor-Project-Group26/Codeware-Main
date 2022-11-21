using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Following
    {
        public int UserId { get; set; }
        public int FollowedId { get; set; }
        [Required]
        public bool IsUser { get; set; }
    }
}
