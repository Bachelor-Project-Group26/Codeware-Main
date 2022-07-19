namespace BPR_Blazor.Data;

public interface IUserService
{
    Task<string> Login(string username, string password);
    //Task<string> Logout(string username);
    Task<string> Register(string username, string password);
    Task<string> UpdateDetails(string username, string token, int securityLevel, string firstName,
            string lastName, string email, string country, string bio, byte[] profilePicture, DateTime birthday);
    Task<string> UpdatePassword(string username, string token, string password);
    Task<string> DeleteUser(string username, string token);
}