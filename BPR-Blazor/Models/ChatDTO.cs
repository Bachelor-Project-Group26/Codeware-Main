namespace BPR_Blazor.Models
{
    [Serializable]
    public class ChatDTO
    {
        public string Username { get; set; }
        public string Username2 { get; set; }
        public int? Id { get; set; }
        public string? ChatName { get; set; }
        public string? Bio { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public MessageDTO? Message { get; set; }
        public List<string>? Users { get; set; }
        public List<MessageDTO>? Messages { get; set; } 
    }
}
