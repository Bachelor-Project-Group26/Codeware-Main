using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPR_API.Controllers
{
    /// <summary>
    /// This is where all the endpoints for the methods related to the post are located and implemented.
    /// </summary>
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

        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("create_post"), Authorize]
        public async Task<ActionResult<string>> CreatePost([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Post newPost = new Post()
            {
                Creator = postDTO.Username,
                FollowedId = postDTO.followedId,
                IsUser = postDTO.isUser,
                Title = postDTO.Title,
                Content = postDTO.Content,
                CreatedDate = postDTO.CreatedDate,
                Likes = 0
            };
            try
            {
                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong with creating the post! Error:" + e.Message + "Inner Exception:" + e.InnerException);
            }
            return Ok("Post created!");
        }

        /// <summary>
        /// Deletes a post.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
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

        /// <summary>
        /// Creates a reaction to a post.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("react_post"), Authorize]
        public async Task<ActionResult<string>> ReactToPost([FromBody] PostDTO postDTO)
        {
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
                _dbContext.Reactions.Add(new Reaction { PostId = postDTO.Id, UserId = user.Id, ReactionNumber = postDTO.Reaction });
                int valueToAdd;
                if (postDTO.Reaction == 1)
                    valueToAdd = 1;
                else
                    valueToAdd = -1;
                var toUpdate = _dbContext.Posts.FirstOrDefault(p => p.Id == postDTO.Id);
                toUpdate.Likes += valueToAdd;
                _dbContext.Posts.Update(toUpdate);
                _dbContext.SaveChanges();
                return Ok("Reaction added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Not implemented!");
        }

        /// <summary>
        /// Gets a post object and returns it.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result with a post object inside if successful or an error message.</returns>
        [HttpPost("get_post"), Authorize]
        public async Task<ActionResult<string>> GetPost([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postDTO.Id);
                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Not implemented!");
        }

        /// <summary>
        /// Gets a list of post objects and returns it.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result with a post list object inside if successful or an error message.</returns>
        [HttpPost("get_post_list"), Authorize]
        public async Task<ActionResult<string>> GetPostListFromUser([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            { 
                var posts = _dbContext.Posts.ToList().Where(p => p.FollowedId == postDTO.followedId && p.Creator == postDTO.Title);
                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        [HttpPost("get_all_posts"), Authorize]
        public async Task<ActionResult<string>> GetPostList([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            { 
                var posts = _dbContext.Posts.ToList();
                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Follows a user or a subject.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("follow"), Authorize]
        public async Task<ActionResult<string>> Follow([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
            var followedUser = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username2);
            Following follower = new Following()
            {
                UserId = user.Id,
                FollowedId = followedUser.Id,
                IsUser = true
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

        /// <summary>
        /// Unollows a user or a subject.
        /// </summary>
        /// <param name="postDTO">Carries data related to the post between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("unfollow"), Authorize]
        public async Task<ActionResult<string>> Unfollow([FromBody] PostDTO postDTO)
        {
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
            var followedUser = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username2);
            try
            {
                var following = _dbContext.FollowingList.FirstOrDefault(f => f.UserId == user.Id && f.FollowedId == followedUser.Id && f.IsUser == true);
                _dbContext.FollowingList.Remove(following);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with unfollowing the user!");
            }
            return Ok("User unfollowed!");
        }

        [HttpPost("send_Comment"), Authorize]
        public async Task<ActionResult<string>> Comment ([FromBody] PostDTO postDTO){
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == postDTO.Username);
                var comment = _dbContext.Comments.Add(new Comment{PostId = postDTO.Id, UserId = user.Id, Username = postDTO.Username,Title = postDTO.Title, Description = postDTO.Content});
                var toUpdate = _dbContext.Posts.FirstOrDefault(p => p.Id == postDTO.Id);
                toUpdate.Comments.Add(comment.Entity);
                _dbContext.Posts.Update(toUpdate);
                await _dbContext.SaveChangesAsync();
                return Ok("Comment added!");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! Error: " + e.Message + "Inner: " + e.InnerException);
            }
        }

        [HttpPost("get_Comments"), Authorize]
        public async Task<ActionResult<string>> GetComments ([FromBody] PostDTO postDTO){
            if (!(postDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var comments = _dbContext.Comments.ToList().Where(c => c.PostId == postDTO.Id);
                return Ok(comments);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! Error: " + e.Message + "Inner: " + e.InnerException);
            }
        }
    }
}
