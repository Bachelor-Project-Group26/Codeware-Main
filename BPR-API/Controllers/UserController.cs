using BPR_API.DBAccess;
using BPR_API.DBModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BPR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserWithPassword userWithPassword)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.Passwords.FirstOrDefault(p => p.Username == userWithPassword.Username);
                if (dbPassword == null) return BadRequest("Username not found!");
                string hash = GenerateHash(userWithPassword.Password, dbPassword.Salt);
                if (hash.Equals(dbPassword.Hash)) return Ok(CreateToken(userWithPassword));
            }
            return BadRequest("Wrong password!");
        }

        private string CreateToken(UserWithPassword user)
        {
            List<Claim> claims = new List<Claim>
            {               
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserWithPassword userWithPassword)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.Passwords.FirstOrDefault(p => p.Username == userWithPassword.Username);
                var dbUser = dbContext.Users.FirstOrDefault(u => u.Username == userWithPassword.Username);
                if (dbPassword != null | dbUser != null) return BadRequest("User already exists!");
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

            return Ok("User created!");
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
