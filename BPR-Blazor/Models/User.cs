namespace BPR_Blazor.Models
{
    [Serializable]
    public class User : Followable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Chat[] Chats { get; set; }

        public User(string username, string password, string name, string email, DateTime birthday, Chat[] chats)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Birthday = birthday;
            Chats = chats;
        }
    }
}
