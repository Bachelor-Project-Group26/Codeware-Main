namespace BPR_Blazor.Models
{
    public class Student : User
    {
        public Student(string username, int securityLevel, string name, string email, DateTime birthday, Chat[] chats) : base(username, securityLevel, name, email, birthday, chats)
        {
        }
    }
}
