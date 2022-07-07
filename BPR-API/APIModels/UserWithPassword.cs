namespace BPR_API.APIModels
{
    [Serializable]
    public class UserWithPassword
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserWithPassword(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
