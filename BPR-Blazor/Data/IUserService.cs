namespace BPR_Blazor.Data;

public interface IUserService
{
    Task<string> Login(string username, string password);
    Task<string> Register(string username, string password);

}