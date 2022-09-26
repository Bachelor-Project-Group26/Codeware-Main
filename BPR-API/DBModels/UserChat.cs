using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPR_API.DBModels
{
    public class UserChat
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int ChatId { get; set; }
    }
}
