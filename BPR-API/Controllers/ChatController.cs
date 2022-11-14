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
    /// This is where all the endpoints for the methods related to the chat are located and implemented.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        /// <summary>
        /// Gets a chat object and returns it.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result with a chat object inside if successful or an error message.</returns>
        [HttpPost("get_chat"), Authorize]
        public async Task<ActionResult<string>> GetChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(userDetails => userDetails.Username == chatDTO.Username);
                var chatList = _dbContext.UserChats.Where(userChat => userChat.UserId == user.Id);
                foreach (UserChat chat in chatList)
                {
                    bool isUserAllowed = false;
                    if (chat.ChatId == chatDTO.Id) 
                    { 
                        var dbChat = _dbContext.Chats.FirstOrDefault(p => p.Id == chatDTO.Id);
                        return Ok(dbChat);
                    }
                }
                return BadRequest("No chat found!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Gets a list of chats and returns it.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result with a chat list object inside if successful or an error message.</returns>
        [HttpPost("get_chat_list"), Authorize]
        public async Task<ActionResult<string>> GetChatList([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == chatDTO.Username);
                var ChatList = _dbContext.UserChats.Where(p => p.UserId == user.Id).ToList();
                var listToReturn = new List<ChatDTO>();
                foreach (UserChat chat in ChatList)
                {
                    var Chat = _dbContext.Chats.ToList().Where(c => c.Id == chat.ChatId);
                    foreach (Chat c in Chat)
                    {
                        var chatToReturn = new ChatDTO()
                        {
                            Id = c.Id,
                            ChatName = c.ChatName,
                        };
                        listToReturn.Add(chatToReturn);
                    }
                }
                return Ok(listToReturn); 
                //var chats = _dbContext.Chats.ToList().Where(p =>p.Username == chatDTO.Username);
                //return Ok(chats);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Creates a chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("create_chat"), Authorize]
        public async Task<ActionResult<string>> CreateChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Chat newChat = new Chat()
            {
                Username = chatDTO.Username,
                ChatName = chatDTO.ChatName,
                Bio = chatDTO.Bio,
                ProfilePicture = new byte[0]
            };
            UserChat userChat;
            try
            {
                await _dbContext.Chats.AddAsync(newChat);
                await _dbContext.SaveChangesAsync();
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == chatDTO.Username);
                var dbChat = _dbContext.Chats.FirstOrDefault(p => p.ChatName == chatDTO.ChatName);
                userChat = new UserChat()
                {
                    UserId = user.Id,
                    ChatId = dbChat.Id
                };
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong with creating the chat! Error:" + e.Message + "Inner Exception:" + e.InnerException);
            }
            try
            {
                await _dbContext.UserChats.AddAsync(userChat);
                await _dbContext.SaveChangesAsync();
            } 
            catch (Exception e)
            {
                await DeleteChat(chatDTO);
                return BadRequest("Something went wrong with adding the user! Error:" + e.Message + "Inner Exception:" + e.InnerException + "chat" + userChat.ChatId + "user" + userChat.UserId);
            }
            return Ok("Chat created!");
        }

        /// <summary>
        /// Updates the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("update_chat"), Authorize]
        public async Task<ActionResult<string>> UpdateChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var dbChatDetails = _dbContext.Chats.FirstOrDefault(u => u.Username == chatDTO.Username);

                dbChatDetails.Username = chatDTO.Username;
                dbChatDetails.ChatName = chatDTO.ChatName;
                dbChatDetails.Bio = chatDTO.Bio;
                // Add picture missing

                _dbContext.Chats.Update(dbChatDetails);
                _dbContext.SaveChanges();

                return Ok("Chat updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Deletes the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("delete_chat"), Authorize]
        public async Task<ActionResult<string>> DeleteChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var to_delete = _dbContext.Chats.FirstOrDefault(u => u.Username == chatDTO.Username);
                _dbContext.Chats.Remove(to_delete);
                _dbContext.UserChats.Remove(_dbContext.UserChats.FirstOrDefault(u => u.ChatId == to_delete.Id));
                _dbContext.SaveChanges();
                return Ok("User deleted successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Adds a user to the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("add_user_chat"), Authorize]
        public async Task<ActionResult<string>> AddUsers([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == chatDTO.Username2);
                _dbContext.UserChats.Add(new UserChat { ChatId = (int) chatDTO.Id, UserId = user.Id });
                _dbContext.SaveChanges();
                return Ok("User added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Removes a user from the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("rem_user_chat"), Authorize]
        public async Task<ActionResult<string>> RemoveUsers([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == chatDTO.Username2);
                _dbContext.UserChats.Remove(new UserChat { ChatId = (int) chatDTO.Id, UserId = user.Id });
                _dbContext.SaveChanges();
                return Ok("User removed!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Get a list of messages from the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result with a list of message object inside if successful or an error message.</returns>
        [HttpPost("get_msg_chat"), Authorize]
        public async Task<ActionResult<string>> GetMessages([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var MessageList = _dbContext.Messages.Where(p => p.ChatID == chatDTO.Id);
                return Ok(MessageList);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Sends a message to the chat.
        /// </summary>
        /// <param name="chatDTO">Carries data related to the chat between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("snd_msg_chat"), Authorize]
        public async Task<ActionResult<string>> SendMessage([FromBody] MessageDTO messageDTO)
        {
            if (!(messageDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                _dbContext.Messages.Add(new Message { 
                    ChatID = messageDTO.ChatID, 
                    Username = messageDTO.Username,
                    Content = messageDTO.Content,
                    CreatedDate = messageDTO.CreatedDate 
                });
                _dbContext.SaveChanges();
                return Ok("User added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
