using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Salt { get; set; }

        public User(string username, string hash, string salt)
        {
            Username = username;
            Hash = hash;
            Salt = salt;
        }
    }
}
