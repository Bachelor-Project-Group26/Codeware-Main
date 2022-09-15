using BPR_API.APIModels;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class ChatService : IChatService
    {
        private string URL = "http://localhost:7000";

        public async Task<string> GetChat(string Username, int Id)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Id = Id
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/get_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public Task<string> GetChatList()
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateChat(string Username, string ChatName, string Bio, byte[] ProfilePicture)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                ChatName = ChatName,
                Bio = Bio,
                ProfilePicture = ProfilePicture
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/create_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> UpdateChat(string Username, string ChatName, string Bio, byte[] ProfilePicture)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                ChatName = ChatName,
                Bio = Bio,
                ProfilePicture = ProfilePicture
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/update_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> DeleteChat(string Username, int Id)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Id = Id
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/delete_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public Task<string> AddUsers()
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveUsers()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMessages()
        {
            throw new NotImplementedException();
        }

        public Task<string> SendMessage()
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteMessages()
        {
            throw new NotImplementedException();
        }
    }
}
