using BPR_API.DBAccess;
using BPR_API.DBModels;

using Microsoft.AspNetCore.Mvc;

namespace BPR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("/user")]
        public async Task<string> Login(string username, string password)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.Passwords.FirstOrDefault(p => p.Username == username);
                if (dbPassword == null) return "Username not found!";
                string hash = GenerateHash(password, dbPassword.Salt);
                if (hash.Equals(dbPassword.Hash)) return "User logged in successfully!";
            }
            return "Wrong password!";
        }

        [HttpPost]
        [Route("/user")]
        public async Task<int> Register([FromBody] UserWithPassword userWithPassword)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.Passwords.FirstOrDefault(p => p.Username == userWithPassword.Username);
                var dbUser = dbContext.Users.FirstOrDefault(u => u.Username == userWithPassword.Username);
                if (dbPassword != null | dbUser != null) return 400;
            }

            User user = new User();

            string salt = GenerateSalt(5);
            string hash = GenerateHash(userWithPassword.Password, salt);
            Password newPassword = new Password(userWithPassword.Password, hash, salt);

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.Passwords.AddAsync(newPassword);
                await dbContext.SaveChangesAsync();
            }

            return 201;
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
