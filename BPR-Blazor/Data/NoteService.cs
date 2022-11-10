using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;
namespace BPR_Blazor.Data
{
    public class NoteService : INoteService
    {
        private string URL = "https://localhost:7000";
        public async Task<string> CreateNote(string username, string title, string noteContent)
        {
            NoteDTO note = new NoteDTO
            {
                Username = username,
                Title = title,
                Content = noteContent,
                CreatedDate = DateTime.Now
            };
            string jsonNote = Newtonsoft.Json.JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Note/create_note", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.Write(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                Console.Write(str);
                return response.StatusCode + str;
            }
        }

        public async Task<string> DeleteNote(string username, int id)
        {
            NoteDTO noteToDelete = new NoteDTO
            {
                Username = username,
                NoteId = id
            };
            string jsonNote = Newtonsoft.Json.JsonConvert.SerializeObject(noteToDelete);
            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Note/delete_note", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<NoteDTO> GetNote(string username, int id)
        {
            NoteDTO note = new NoteDTO();
            NoteDTO user = new NoteDTO
            {
                Username = username,
                NoteId = id
            };
            string jsonNote = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{URL}/Note/{id}" + "?username" + "=" + username))
            {
                var json = await response.Content.ReadAsStringAsync();

                note = JsonConvert.DeserializeObject<NoteDTO>(json);

                return note;
            }
        }

        public async Task<List<NoteDTO>> GetNoteList(string username)
        {
            List<NoteDTO> notes = new List<NoteDTO>();
            NoteDTO note = new NoteDTO
            {
                Username = username,
                NoteId = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now
            };
            string jsonNote = Newtonsoft.Json.JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Note/get_notes", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                notes = JsonConvert.DeserializeObject<List<NoteDTO>>(json);
                return notes;
            }
        }

        public async Task<string> UpdateNote(string username, int id, string title, string noteContent)
        {
            NoteDTO note = new NoteDTO
            {
                Username = username,
                NoteId = id,
                Title = title,
                Content = noteContent,
                CreatedDate = DateTime.Now
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/Post/update_note", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
    }
}