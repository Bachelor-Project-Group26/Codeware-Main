using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    [Serializable]
    public class UserDetailsDB
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        public int? SecurityLevel { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [Timestamp]
        public DateTime? Birthday { get; set; }
    }
}
