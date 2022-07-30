namespace BPR_Blazor.Models
{
    [Serializable]
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public DateTime LastMessage { get; set; }
        public List<MessageDTO> Messages { get; set; }

        public ChatDTO(int id, string name, DateTime lastMessage, MessageDTO[] messages)
        {
            ChatId = 0;
            Name = "";
            LastMessage = DateTime.Now;
            Messages = new List<MessageDTO>();
        }
    }
}
