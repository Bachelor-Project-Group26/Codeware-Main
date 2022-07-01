namespace BPR_Blazor.Models
{
    [Serializable]
    public class Message
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public Message(int id, string username, string content, DateTime createdDate)
        {
            Id = id;
            Username = username;
            Content = content;
            CreatedDate = createdDate;
        }
    }
}
