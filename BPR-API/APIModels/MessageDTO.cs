namespace BPR_API.APIModels
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int ChatID { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
