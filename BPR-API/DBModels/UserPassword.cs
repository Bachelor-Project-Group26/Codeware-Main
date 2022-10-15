using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class UserPassword
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(50)]
        public string Hash { get; set; }
        [Required, MaxLength(50)]
        public string Salt { get; set; }

        public UserPassword(string username, string hash, string salt)
        {
            Username = username;
            Hash = hash;
            Salt = salt;
        }
    }
}
