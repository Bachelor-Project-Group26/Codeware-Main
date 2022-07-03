namespace BPR_Blazor.Models
{
    [Serializable]
    public class User : Followable
    {
        public string Username { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Chat[] Chats { get; set; }

        public User(string username, int securityLevel, string name, string email, DateTime birthday, Chat[] chats)
        {
            Username = username;
            SecurityLevel = securityLevel;
            Name = name;
            Email = email;
            Birthday = birthday;
            Chats = chats;
        }
    }
}
