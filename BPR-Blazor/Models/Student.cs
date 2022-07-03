namespace BPR_Blazor.Models
{
    public class Student : User
    {
        public Student(string username, string password, string name, string email, DateTime birthday, Chat[] chats) : base(username, password, name, email, birthday, chats)
        {
        }
    }
}
