using BPR_Blazor.Models;

namespace BPR_Blazor.Data;

public interface IUserService
{
    Task<string> Login(string username, string password);
    string Logout(string username);
    Task<string> Register(string username, string password);
    Task<string> GetUserByUsername(string username);
    Task<string> UpdateDetails(string username, int securityLevel, string firstName,
            string lastName, string email, string country, string bio, byte[] profilePicture, DateTime birthday);
    Task<string> UpdatePassword(string username, string password);
    // Task<List<UserDTO>> GetAllUsers();
    Task<string> DeleteUser(string username);
}