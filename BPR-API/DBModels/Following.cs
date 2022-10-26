using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Following
    {
        public int UserId { get; set; }
        public string FollowedId { get; set; } // US{userId} for users | SU{subjectId} for subjects
    }
}
