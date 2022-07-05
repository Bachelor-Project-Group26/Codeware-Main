namespace BPR_API.DBModels
{
    public class UserWithPassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public UserWithPassword(string username, string password, int securityLevel, string name, string email, DateTime birthday)
        {
            Username = username;
            Password = password;
            SecurityLevel = securityLevel;
            Name = name;
            Email = email;
            Birthday = birthday;
        }
    }
}
