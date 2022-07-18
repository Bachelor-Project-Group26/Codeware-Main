namespace BPR_API.APIModels
{
    [Serializable]
    public class UserDTO
    {
        public string Username { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public int? SecurityLevel { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
