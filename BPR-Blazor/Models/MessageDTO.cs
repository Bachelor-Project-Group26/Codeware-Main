namespace BPR_Blazor.Models
{
    public class MessageDTO
    {
        public string Username { get; set; }
        public int? Id { get; set; }
        public int ChatID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
