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
            catch 
            { 
                return BadRequest("Something went wrong with creating the subject!"); 
            }
            var own = new SubjectOwner()
            {
                UserId = 1,
                SubjectId = 1
            };
            var follow = new Following()
            {
                UserId = 1,
                FollowedId = ""
            };
            try
            {
                _dbContext.SubjectOwners.Add(own);
                _dbContext.FollowingList.Add(follow);
                _dbContext.SaveChanges();
            }
            catch
            {
                return BadRequest("Something went wrong with adding user to subject!");
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
                var followingList = _dbContext.FollowingList.Where(f => f.FollowedId == subjectDTO.SubjectId.ToString());
                foreach (var follow in followingList)
                {
                    var to_remove = new Following()
                    {
                        UserId = follow.UserId,
                        FollowedId = follow.FollowedId
                    };
                    _dbContext.FollowingList.Remove(to_remove);
                }
                var ownerList = _dbContext.SubjectOwners.Where(f => f.UserId == subjectDTO.UserId);
                foreach (var owner in ownerList)
                {
                    var to_remove = new SubjectOwner()
                    {
                        UserId = owner.UserId,
                        SubjectId = owner.SubjectId
                    };
                    _dbContext.SubjectOwners.Remove(to_remove);
                }
            }
            catch
            {
                return BadRequest("Something went wrong with deleting the users from the subject!");
            }
            try
            {
                _dbContext.Subjects.Remove(_dbContext.Subjects.FirstOrDefault(u => u.Id == subjectDTO.SubjectId));
                _dbContext.SaveChanges();
            }
            catch
            {
                return BadRequest("Something went wrong with deleting the subject!");
            }
            return Ok("Subject deleted successfully!");
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
            catch
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
            try
            {
                var owner_to_delete = _dbContext.SubjectOwners.FirstOrDefault(o => o.SubjectId == subjectDTO.SubjectId);
                _dbContext.SubjectOwners.Remove(owner_to_delete);
            }
            catch
            {
                return BadRequest("Something went wrong with deleting the owner!");
            }
            return Ok("Not implemented!");
        }
    }
}
