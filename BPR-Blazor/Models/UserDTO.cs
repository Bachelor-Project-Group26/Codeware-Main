namespace BPR_Blazor.Models
{
    [Serializable]
    public class UserDTO
    {
        public string Username { get; set; }
        public int? SecurityLevel { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Image { get; set; }
    }
}
