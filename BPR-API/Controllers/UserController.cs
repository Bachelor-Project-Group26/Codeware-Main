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
    /// <summary>
    /// This is where all the endpoints for the methods related to the user are located and implemented.
    /// </summary>
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

        public UserController(IConfiguration configuration, DatabaseContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Checks the credentials of the user and logs them in.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result with a JSON Web Token if successful or an error message.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDTO userDTO)
        {
            try
            {
                var dbPassword = _dbContext.UserPasswords.FirstOrDefault(p => p.Username == userDTO.Username);
                if (dbPassword == null) return BadRequest("Username not found!");
                string hash = Authentication.GenerateHash(userDTO.Password, dbPassword.Salt);
                if (hash.Equals(dbPassword.Hash)) return Ok(Authentication.CreateToken(userDTO, _configuration));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return BadRequest("Wrong password!");
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("register")]
        public ObjectResult Register([FromBody] UserDTO userDTO)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var dbPassword = dbContext.UserPasswords.FirstOrDefault(p => p.Username == userDTO.Username);
                var dbUser = dbContext.UserDetails.FirstOrDefault(u => u.Username == userDTO.Username);
                if (dbPassword != null | dbUser != null) return BadRequest("User already exists!");
            }

            UserDetails userDetails = new UserDetails()
            {
                Username = userDTO.Username
            };

            string salt = Authentication.GenerateSalt(5);
            string hash = Authentication.GenerateHash(userDTO.Password, salt);
            UserPassword userPassword = new UserPassword(userDTO.Username, hash, salt);

            try
            {
                _dbContext.UserDetails.AddAsync(userDetails);
                _dbContext.UserPasswords.AddAsync(userPassword);
                _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

            return Ok("User created!");
        }

        /// <summary>
        /// Searches for a user using their username.
        /// </summary>
        /// <param name="username">The username of the user that we want to find.</param>
        /// <returns>Action result and a string with message regarding the action result or the inforamtion regarding the user.</returns>
        [HttpGet("{username}")]
        public ObjectResult GetUserByUsername(string username)
        {
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == username);
                if (user == null) return BadRequest("User not found!");
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! Error:" + e.Message);
            }
        }

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetAllUsers()
        {
            try
            {
                var users = _dbContext.UserDetails.ToList();
                if (users == null) return BadRequest("No users found!");
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! Error:" + e.Message);
            }
        }

        /// <summary>
        /// Updates the details regarding a user.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("update_details"), Authorize]
        public ObjectResult UpdateDetails([FromBody] UserDTO userDTO)
        {
            if (!(userDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var dbUserDetails = _dbContext.UserDetails.FirstOrDefault(u => u.Username == userDTO.Username);

                if (userDTO.SecurityLevel != null) dbUserDetails.SecurityLevel = userDTO.SecurityLevel;
                if (userDTO.FirstName != null) dbUserDetails.FirstName = userDTO.FirstName;
                if (userDTO.LastName != null) dbUserDetails.LastName = userDTO.LastName;
                if (userDTO.Email != null) dbUserDetails.Email = userDTO.Email;
                if (userDTO.Country != null) dbUserDetails.Country = userDTO.Country;
                if (userDTO.Bio != null) dbUserDetails.Bio = userDTO.Bio;
                // Add picture missing
                if (userDTO.Birthday != null) dbUserDetails.Birthday = userDTO.Birthday;

                _dbContext.UserDetails.Update(dbUserDetails);
                _dbContext.SaveChanges();

                return Ok("Details updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Updates the password of a user.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("update_password"), Authorize]
        public async Task<ActionResult<string>> UpdatePassword([FromBody] UserDTO userDTO)
        {
            if (!(userDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var dbPassword = _dbContext.UserPasswords.FirstOrDefault(u => u.Username == userDTO.Username);

                dbPassword.Salt = Authentication.GenerateSalt(5);
                dbPassword.Hash = Authentication.GenerateHash(userDTO.Password, dbPassword.Salt);

                _dbContext.UserPasswords.Update(dbPassword);
                _dbContext.SaveChanges();
                return Ok("Password updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
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
