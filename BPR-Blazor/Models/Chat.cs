namespace BPR_Blazor.Models
{
    [Serializable]
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastMessage { get; set; }
        public Message[] Messages { get; set; }

        public Chat(int id, string name, DateTime lastMessage, Message[] messages)
        {
            Id = id;
            Name = name;
            LastMessage = lastMessage;
            Messages = messages;
        }
    }
}
