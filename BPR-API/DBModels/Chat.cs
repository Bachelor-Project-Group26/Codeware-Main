namespace BPR_API.DBModels
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public IList<Message> Messages { get; set; }
    }
}
