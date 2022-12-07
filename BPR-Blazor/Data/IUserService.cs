using BPR_Blazor.Models;

namespace BPR_Blazor.Data;

public interface IUserService
{
    Task<string> Login(string username, string password);
    void Logout();
    Task<string> Register(string username, string password);
    Task<List<UserDTO>> GetUsers();
    Task<UserDTO> GetUserByUsername(string username);
    Task<string> UpdateDetails(string username, int securityLevel, string firstName,
            string lastName, string email, string country, string bio, byte[] profilePicture, DateTime? birthday, string image);
    Task<string> UpdatePassword(string username, string password);
    Task<string> DeleteUser(string username);
}