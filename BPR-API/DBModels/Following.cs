using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Following
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public string FollowedId { get; set; } // US{userId} for users | SU{subjectId} for subjects
    }
}
