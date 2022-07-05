﻿namespace BPR_Blazor.Models
{
    [Serializable]
    public class User : Followable
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SecurityLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public User(int userId, string username, int securityLevel, string name, string email, DateTime birthday)
        {
            UserId = userId;
            Username = username;
            SecurityLevel = securityLevel;
            Name = name;
            Email = email;
            Birthday = birthday;
        }
    }
}
