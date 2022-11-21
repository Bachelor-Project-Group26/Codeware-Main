using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BPR_API.Controllers
{
    /// <summary>
    /// This is where all the endpoints for the methods related to the notes are located and implemented.
    /// </summary>
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

        /// <summary>
        /// Creates a note.
        /// </summary>
        /// <param name="noteDTO">Carries data related to the note between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
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

        /// <summary>
        /// Deletes a note.
        /// </summary>
        /// <param name="noteDTO">Carries data related to the note between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPost("delete_note"), Authorize]
        public async Task<ActionResult<string>> DeleteNote([FromBody] NoteDTO noteDTO)
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

        /// <summary>
        /// Updates a note.
        /// </summary>
        /// <param name="noteDTO">Carries data related to the note between the client and the API.</param>
        /// <returns>Action result and a string with message regarding the action result.</returns>
        [HttpPut("update_note"), Authorize]
        public async Task<ActionResult<string>> UpdateNote([FromBody] NoteDTO noteDTO)
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

        /// <summary>
        /// Gets a note object and returns it.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="id">The id of the note.</param>
        /// <returns>Action result with a note object inside if successful or an error message.</returns>
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

        /// <summary>
        /// Gets a list of note objects and returns it.
        /// </summary>
        /// <param name="noteDTO">Carries data related to the note between the client and the API.</param>
        /// <returns>Action result with a note list object inside if successful or an error message.</returns>
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