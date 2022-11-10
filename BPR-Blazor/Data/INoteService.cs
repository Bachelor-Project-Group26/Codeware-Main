using BPR_Blazor.Models;
namespace BPR_Blazor.Data
{
    public interface INoteService
    {
        public Task<string> CreateNote(string username, string title, string noteContent);
        public Task<string> DeleteNote(string username, int id);

        public Task<NoteDTO> GetNote(string username, int id);
        public Task<List<NoteDTO>> GetNoteList(string username);
        
        public Task<string> UpdateNote(string username, int id, string title, string noteContent);
    }
}