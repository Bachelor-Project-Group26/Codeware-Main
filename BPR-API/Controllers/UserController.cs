using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
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
        private DatabaseContext _dbContext;

        public UserController(IConfiguration configuration) {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        [HttpGet("test")]
        public async Task<ActionResult<string>> test()
        {
            return Ok("The API works!");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDTO user)
        {
            try
            {
                var dbPassword = _dbContext.UserPasswords.FirstOrDefault(p => p.Username == user.Username);
                if (dbPassword == null) return BadRequest("Username not found!");
                string hash = Authentication.GenerateHash(user.Password, dbPassword.Salt);
                if (hash.Equals(dbPassword.Hash)) return Ok(Authentication.CreateToken(user, _configuration));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return BadRequest("Wrong password!");
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserDTO user)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.UserPasswords.FirstOrDefault(p => p.Username == user.Username);
                var dbUser = dbContext.UserDetails.FirstOrDefault(u => u.Username == user.Username);
                if (dbPassword != null | dbUser != null) return BadRequest("User already exists!");
            }

            UserDetails userDetails = new UserDetails()
            {
                Username = user.Username
            };

            string salt = Authentication.GenerateSalt(5);
            string hash = Authentication.GenerateHash(user.Password, salt);
            UserPassword userPassword = new UserPassword(user.Username, hash, salt);

            try
            {
                await _dbContext.UserDetails.AddAsync(userDetails);
                await _dbContext.UserPasswords.AddAsync(userPassword);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

            return Ok("User created!");
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<string>> GetUserByUsername(string username)
        {
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == username);
                if (user == null) return BadRequest("User not found!");
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<string>> GetAllUsers()
        {
            try
            {
                var users = _dbContext.UserDetails.ToList();
                if (users == null) return BadRequest("No users found!");
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("update_details"), Authorize]
        public async Task<ActionResult<string>> UpdateDetails([FromBody] UserDTO user)
        {
            if (!(user.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var dbUserDetails = _dbContext.UserDetails.FirstOrDefault(u => u.Username == user.Username);

                if (user.SecurityLevel != null) dbUserDetails.SecurityLevel = user.SecurityLevel;
                if (user.FirstName != null) dbUserDetails.FirstName = user.FirstName;
                if (user.LastName != null) dbUserDetails.LastName = user.LastName;
                if (user.Email != null) dbUserDetails.Email = user.Email;
                if (user.Country != null) dbUserDetails.Country = user.Country;
                if (user.Bio != null) dbUserDetails.Bio = user.Bio;
                // Add picture missing
                if (user.Birthday != null) dbUserDetails.Birthday = user.Birthday;

                _dbContext.UserDetails.Update(dbUserDetails);
                _dbContext.SaveChanges();

                return Ok("Details updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("update_password"), Authorize]
        public async Task<ActionResult<string>> UpdatePassword([FromBody] UserDTO user)
        {
            if (!(user.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var dbPassword = _dbContext.UserPasswords.FirstOrDefault(u => u.Username == user.Username);

                dbPassword.Salt = Authentication.GenerateSalt(5);
                dbPassword.Hash = Authentication.GenerateHash(user.Password, dbPassword.Salt);

                _dbContext.UserPasswords.Update(dbPassword);
                _dbContext.SaveChanges();
                return Ok("Password updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("delete"), Authorize]
        public async Task<ActionResult<string>> DeleteUser([FromBody] UserDTO user)
        {
            if (!(user.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                _dbContext.UserPasswords.Remove(_dbContext.UserPasswords.FirstOrDefault(u => u.Username == user.Username));
                _dbContext.UserDetails.Remove(_dbContext.UserDetails.FirstOrDefault(u => u.Username == user.Username));
                _dbContext.SaveChanges();
                return Ok("User deleted successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
