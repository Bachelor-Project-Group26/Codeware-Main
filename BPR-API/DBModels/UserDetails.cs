using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Timestamp]
        public DateTime Birthday { get; set; }
    }
}
