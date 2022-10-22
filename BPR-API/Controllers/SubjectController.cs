using BPR_API.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPR_API.Controllers
{
    /// <summary>
    /// This is where all the endpoints for the methods related to the subject are located and implemented.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public SubjectController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        [HttpPost("create_subject"), Authorize]
        public async Task<ActionResult<string>> createSubject()
        {
            return Ok("Not implemented!");
        }

        [HttpPut("delete_subject"), Authorize]
        public async Task<ActionResult<string>> deleteSubject()
        {
            //if (!(user.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            //try
            //{
            //    _dbContext.UserPasswords.Remove(_dbContext.UserPasswords.FirstOrDefault(u => u.Username == user.Username));
            //    _dbContext.UserDetails.Remove(_dbContext.UserDetails.FirstOrDefault(u => u.Username == user.Username));
            //    _dbContext.SaveChanges();
            //    return Ok("User deleted successfully!");
            //}
            //catch (Exception)
            //{
            //    return BadRequest("Something went wrong!");
            //}
            return Ok("Not implemented!");
        }

        [HttpPost("add_owner"), Authorize]
        public async Task<ActionResult<string>> addOwner()
        {
            return Ok("Not implemented!");
        }

        [HttpPut("del_owner"), Authorize]
        public async Task<ActionResult<string>> deleteOwner()
        {
            return Ok("Not implemented!");
        }

        [HttpPost("add_student"), Authorize]
        public async Task<ActionResult<string>> addStudent()
        {
            return Ok("Not implemented!");
        }

        [HttpPut("del_student"), Authorize]
        public async Task<ActionResult<string>> deleteStudent()
        {
            return Ok("Not implemented!");
        }
    }
}
