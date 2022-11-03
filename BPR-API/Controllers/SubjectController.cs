using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
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

        /// <summary>
        /// Creates a subject.
        /// </summary>
        /// <param name="subjectDTO">Carries data related to the subject between the client and the API.</param>
        /// <returns></returns>
        [HttpPost("create_subject"), Authorize]
        public async Task<ActionResult<string>> CreateSubject(SubjectDTO subjectDTO)
        {
            if (!(subjectDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Subject newSubject = new Subject()
            {
                Name = subjectDTO.Name,
                SubjectCode = subjectDTO.SubjectCode
            };
            try
            {
                await _dbContext.Subjects.AddAsync(newSubject);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with creating the subject!");
            }
            return Ok("Subject created!");
        }

        /// <summary>
        /// Deletes a subject.
        /// </summary>
        /// <param name="subjectDTO">Carries data related to the subject between the client and the API.</param>
        /// <returns></returns>
        [HttpPut("delete_subject"), Authorize]
        public async Task<ActionResult<string>> DeleteSubject(SubjectDTO subjectDTO)
        {
            if (!(subjectDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                _dbContext.Subjects.Remove(_dbContext.Subjects.FirstOrDefault(u => u.Id == subjectDTO.SubjectId));
                _dbContext.SaveChanges();
                return Ok("Subject deleted successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            // Delete the users
            // Delete the owners
        }

        /// <summary>
        /// Adds an owner to the subject.
        /// </summary>
        /// <param name="subjectDTO">Carries data related to the subject between the client and the API.</param>
        /// <returns></returns>
        [HttpPost("add_owner"), Authorize]
        public async Task<ActionResult<string>> AddOwner(SubjectDTO subjectDTO)
        {
            if (!(subjectDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            SubjectOwner newOwner = new SubjectOwner()
            {
                UserId = subjectDTO.UserId,
                SubjectId = subjectDTO.SubjectId
            };
            Following follower = new Following()
            {
                UserId = subjectDTO.UserId,
                FollowedId = "US1234"
            };
            try
            {
                await _dbContext.SubjectOwners.AddAsync(newOwner);
                await _dbContext.FollowingList.AddAsync(follower);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with adding the owner!");
            }
            return Ok("Owner added!");
        }

        /// <summary>
        /// Deletes an owner from the subject.
        /// </summary>
        /// <param name="subjectDTO">Carries data related to the subject between the client and the API.</param>
        /// <returns></returns>
        [HttpPut("del_owner"), Authorize]
        public async Task<ActionResult<string>> DeleteOwner(SubjectDTO subjectDTO)
        {
            // Delete from owner table.
            return Ok("Not implemented!");
        }
    }
}
