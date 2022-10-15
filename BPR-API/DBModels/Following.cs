using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Following
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public string FollowedId { get; set; } // US{id} for users | SU{id} for subjects
    }
}
