namespace BPR_Blazor.Models
{
    public class Professor : User
    {
        public Professor(string username, int securityLevel, string name, string email, DateTime birthday, Chat[] chats) : base(username, securityLevel, name, email, birthday, chats)
        {
        }
    }
}
