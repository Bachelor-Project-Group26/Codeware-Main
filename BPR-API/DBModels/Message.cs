using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int ChatID { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
