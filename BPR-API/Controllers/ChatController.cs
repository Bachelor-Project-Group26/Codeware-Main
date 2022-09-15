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
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        [HttpPost("get_chat"), Authorize]
        public async Task<ActionResult<string>> GetChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == chatDTO.Username);
                var chatList = _dbContext.UserChats.SelectMany(p => p.UserId == user.Id);
                foreach (UserChat chat in chatList)
                {
                    bool isUserAllowed = false;
                    if (chat.ChatId == chatDTO.Id) 
                    { 
                        var dbChat = _dbContext.Chats.FirstOrDefault(p => p.Id == chatDTO.Id);
                        return Ok(chat); // I need to find how to serialize this!
                    }
                }
                return BadRequest("No chat found!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost("get_chat_list"), Authorize]
        public async Task<ActionResult<string>> GetChatList([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == chatDTO.Username);
                var ChatList = _dbContext.UserChats.FirstOrDefault(p => p.UserId == user.Id);
                return Ok(ChatList); // I need to find how to serialize this!
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Test");
        }

        [HttpPost("create_chat"), Authorize]
        public async Task<ActionResult<string>> CreateChat([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Chat newChat = new Chat()
            {
                Username = chatDTO.Username,
                ChatName = chatDTO.ChatName,
                Bio = chatDTO.Bio,
                ProfilePicture = chatDTO.ProfilePicture
            };
            UserChat userChat;
            try
            {
                await _dbContext.Chats.AddAsync(newChat);
                await _dbContext.SaveChangesAsync();
                var user = _dbContext.UserDetails.FirstOrDefault(p => p.Username == chatDTO.Username);
                var dbChat = _dbContext.UserPasswords.FirstOrDefault(p => p.Username == user.Username);
                userChat = new UserChat()
                {
                    UserId = user.Id,
                    ChatId = dbChat.Id
                };
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with creating the chat!");
            }
            try
            {
                await _dbContext.UserChats.AddAsync(userChat);
                await _dbContext.SaveChangesAsync();
            } 
            catch (Exception)
            {
                await DeleteChat(chatDTO);
                return BadRequest("Something went wrong with adding the user!");
            }
            return Ok("Chat created!");
        }

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
                dbChatDetails.ProfilePicture = chatDTO.ProfilePicture;

                _dbContext.Chats.Update(dbChatDetails);
                _dbContext.SaveChanges();

                return Ok("Chat updated successfully!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        
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

        [HttpPut("add_user_chat"), Authorize]
        public async Task<ActionResult<string>> AddUsers([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Username == chatDTO.Username);
                _dbContext.UserChats.Add(new UserChat { ChatId = chatDTO.Id, UserId = user.Id });
                _dbContext.SaveChanges();
                return Ok("User added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("rem_user_chat"), Authorize]
        public async Task<ActionResult<string>> RemoveUsers([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var user = _dbContext.UserDetails.FirstOrDefault();
                _dbContext.UserChats.Remove(new UserChat { ChatId = chatDTO.Id, UserId = user.Id });
                _dbContext.SaveChanges();
                return Ok("User removed!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost("get_msg_chat"), Authorize]
        public async Task<ActionResult<string>> GetMessages([FromBody] ChatDTO chatDTO)
        {
            return Ok("Not implemented!");
        }

        [HttpPost("snd_msg_chat"), Authorize]
        public async Task<ActionResult<string>> SendMessage([FromBody] MessageDTO messageDTO)
        {
            if (!(messageDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                // var user = _dbContext.UserDetails.FirstOrDefault();
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
        
        [HttpPost("del_msg_chat"), Authorize]
        public async Task<ActionResult<string>> DeleteMessages([FromBody] ChatDTO chatDTO)
        {
            if (!(chatDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var msg = _dbContext.Messages.FirstOrDefault(m => m.Id == chatDTO.Message.Id);
                if (chatDTO.Username == msg.Username) _dbContext.Messages.Remove(msg);
                else return Unauthorized("Unauthorized user!");
                _dbContext.SaveChanges();
                return Ok("User removed!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
