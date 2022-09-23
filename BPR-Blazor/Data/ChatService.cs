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
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/get_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> GetChatList(string Username)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/get_chat_list", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
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
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
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
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
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
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/delete_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> AddUsers(string Username, string Username2, int Id)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Username2 = Username2,
                Id = Id
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/add_user_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> RemoveUsers(string Username, string Username2, int Id)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Username2 = Username2, 
                Id = Id
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/rem_user_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> GetMessages(string Username, int Id)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Id = Id
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/get_msg_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> SendMessage(string Username, int Id, string Content)
        {
            MessageDTO messageDTO = new MessageDTO
            {
                Username = Username,
                ChatID = Id,
                Content = Content,
                CreatedDate = DateTime.Now
            };
            string jsonMessageDTO = Newtonsoft.Json.JsonConvert.SerializeObject(messageDTO);
            StringContent content = new StringContent(jsonMessageDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/snd_msg_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
    }
}
