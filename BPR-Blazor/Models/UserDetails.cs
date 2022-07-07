namespace BPR_Blazor.Models
{
    [Serializable]
    public class UserDetails
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public UserDetails(int id, string username, int securityLevel, string name, string email, DateTime birthday)
        {
            Id = id;
            Username = username;
            SecurityLevel = securityLevel;
            Name = name;
            Email = email;
            Birthday = birthday;
        }
    }
}
