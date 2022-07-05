namespace BPR_API.DBModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
