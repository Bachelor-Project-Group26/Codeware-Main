namespace BPR_API.DBModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
