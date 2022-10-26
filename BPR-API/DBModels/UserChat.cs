using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPR_API.DBModels
{
    public class UserChat
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
    }
}
