using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}