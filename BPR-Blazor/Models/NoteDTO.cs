namespace BPR_Blazor.Models
{
    public class NoteDTO
    {
        public string Username { get; set; }
        public int? NoteId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
