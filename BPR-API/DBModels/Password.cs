namespace BPR_API.DBModels
{
    public class Password
    {
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public Password(string username, string hash, string salt)
        {
            Username = username;
            Hash = hash;
            Salt = salt;
        }
    }
}
