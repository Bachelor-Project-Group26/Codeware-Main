namespace BPR_Blazor.Models
{
    public class Professor : User
    {
        public Professor(string username, string password, string name, string email, DateTime birthday, Chat[] chats) : base(username, password, name, email, birthday, chats)
        {
        }
    }
}
