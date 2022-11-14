using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BPR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NoteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public NoteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }

        [HttpPost("create_note"), Authorize]
        public async Task<ActionResult<string>> CreateNote([FromBody] NoteDTO noteDTO)
        {
            if (!(noteDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            Note newNote = new Note()
            {
                Username = noteDTO.Username,
                Title = noteDTO.Title,
                Content = noteDTO.Content,
                CreatedDate = noteDTO.CreatedDate
            };
            try
            {
                await _dbContext.Notes.AddAsync(newNote);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong with creating the Note!");
            }
            return Ok("Note created!");
        }

        [HttpPost("delete_note"), Authorize]
        public async Task<ActionResult<string>> DeletePost([FromBody] NoteDTO noteDTO)
        {
            if (!(noteDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var to_delete = _dbContext.Notes.FirstOrDefault(u => u.NoteId == noteDTO.NoteId);
                _dbContext.Notes.Remove(to_delete);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok("Post deleted successfully!");
        }

        [HttpPut("update_note"), Authorize]
        public async Task<ActionResult<string>> UpdatePost([FromBody] NoteDTO noteDTO)
        {
            if (!(noteDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var to_update = _dbContext.Notes.FirstOrDefault(u => u.NoteId == noteDTO.NoteId);
                if (noteDTO.Title != "") to_update.Title = noteDTO.Title;
                if (noteDTO.Content != "") to_update.Content = noteDTO.Content;
                to_update.CreatedDate = noteDTO.CreatedDate;
                 _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! + Inner Exception : " + e.InnerException + "Message : " + e.Message);
            }
            return Ok("Post updated successfully!");
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<NoteDTO>> GetNote(string username, int id)
        {
            if (!(username== User?.Identity?.Name)) return Unauthorized("Token invalid!");
            NoteDTO noteDTO = new NoteDTO();
            try
            {
                var note = _dbContext.Notes.Where(u => u.Username == username).FirstOrDefault(u => u.NoteId == id);
                noteDTO.Username = note.Username;
                noteDTO.NoteId = note.NoteId;
                noteDTO.Title = note.Title;
                noteDTO.Content = note.Content;
                noteDTO.CreatedDate = note.CreatedDate;
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok(noteDTO);
        }

        [HttpPost("get_notes"), Authorize]
        public async Task<ActionResult<List<NoteDTO>>> GetAllNotes([FromBody] NoteDTO noteDTO)
        {
            if (!(noteDTO.Username == User?.Identity?.Name)) return Unauthorized("Token invalid!");
            try
            {
                var all_notes = _dbContext.Notes.ToList().Where(u => u.Username == noteDTO.Username);
                return Ok(all_notes);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}