using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    [Serializable]
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        public int? SecurityLevel { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Country { get; set; }
        [MaxLength(250)]
        public string? Bio { get; set; }
        public byte[]? ProfilePicture { get; set; }
        [Timestamp]
        public DateTime? Birthday { get; set; }
    }
}
