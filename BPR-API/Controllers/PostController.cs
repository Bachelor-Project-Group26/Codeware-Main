using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        [HttpPost("create_post"), Authorize]
        public async Task<ActionResult<string>> CreatePost([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Post newPost = new Post()
            {
                Creator = postDTO.Username,
                Title = "Test",
                Content = postDTO.Content,
                CreatedDate = postDTO.CreatedDate
            };
            try
            {
                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with creating the post!");
            }
            return Ok("Post created!");
        }

        [HttpPost("delete_post"), Authorize]
        public async Task<ActionResult<string>> DeletePost([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var to_delete = _dbContext.Posts.FirstOrDefault(u => u.Id == postDTO.Id);
                _dbContext.Posts.Remove(to_delete);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Post deleted successfully!");
        }

        [HttpPost("react_post"), Authorize]
        public async Task<ActionResult<string>> ReactToPost([FromBody] PostDTO postDTO)
        {
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
                var post = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
                _dbContext.Reactions.Add(new Reaction { PostId = (int) post.Id, UserId = user.Id, ReactionNumber = postDTO.Reaction });
                _dbContext.SaveChanges();
                return Ok("Reaction added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Not implemented!");
        }

        [HttpPost("get_post"), Authorize]
        public async Task<ActionResult<string>> GetPost([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == postDTO.Username);
                var ChatList = _dbContext.UserChats.Where(p => p.UserId == user.Id);
                return Ok(ChatList); // I need to find how to serialize this!
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Not implemented!");
        }

        [HttpPost("get_post_list"), Authorize]
        public async Task<ActionResult<string>> GetPostList([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var Following = _dbContext.FollowingList.Where(u => u.UserId == postDTO.Id);
                // var Posts = _dbContext.Posts.Where(p => p.FollowedId == Following.Contains FollowedId);
                // return Ok(Posts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Not implemented!");
        }

        [HttpPost("follow"), Authorize]
        public async Task<ActionResult<string>> follow([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
            Following follower = new Following()
            {
                UserId = user.Id,
                FollowedId = "US1234"
            };
            try
            {
                await _dbContext.FollowingList.AddAsync(follower);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with following the user!");
            }
            return Ok("User followed!");
        }
        
        [HttpPut("unfollow"), Authorize]
        public async Task<ActionResult<string>> unfollow([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
            var followedId = "US1234";
            try
            {
                var following = _dbContext.FollowingList.FirstOrDefault(f => f.UserId == user.Id && f.FollowedId == followedId);
                _dbContext.FollowingList.Remove(following);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with unfollowing the user!");
            }
            return Ok("User unfollowed!");
        }
    }
}
