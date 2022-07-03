using Microsoft.AspNetCore.Mvc;

namespace BPR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public Task<string> Login(string username, string password)
        {
            return null;
        }

        [HttpPost]
        public Task<string> Register(string username, string password)
        {
            string salt = GenerateSalt(5);
            string hash = GenerateHash(password, salt);
            // save username, salt, and hash
            return null;
        }

        private string GenerateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new Byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private string GenerateHash(string password, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = 
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            
            return System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        }
    }
}
