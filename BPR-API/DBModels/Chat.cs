using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string ChatName { get; set; }
        public string Bio { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
