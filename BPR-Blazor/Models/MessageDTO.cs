namespace BPR_Blazor.Models
{
    [Serializable]
    public class MessageDTO
    {
        public int ChatID { get; set; }
        public string CreatorUsername { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public MessageDTO()
        {
            ChatID = 0;
            CreatorUsername = "";
            Content = "";
            CreatedDate = DateTime.Now;
        }
    }
}
