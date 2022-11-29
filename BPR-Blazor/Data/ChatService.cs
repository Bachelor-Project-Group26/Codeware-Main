using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class ChatService : IChatService
    {
       // private string URL = "https://codeware-backend-bpr.azurewebsites.net/";
        private string URL = "https://localhost:7000";

        public async Task<ChatDTO> GetChat(string Username, int Id)
        {   
            ChatDTO chat = new ChatDTO();
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Id = Id
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/get_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                chat = JsonConvert.DeserializeObject<ChatDTO>(json);
                return chat; 
            }
        }

        public async Task<List<ChatDTO>> GetChatList(string Username)
        {
            List<ChatDTO> chatDTOs = new List<ChatDTO>();
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/get_chat_list", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                chatDTOs = JsonConvert.DeserializeObject<List<ChatDTO>>(json);
                return chatDTOs;
            }
        }

        public async Task<string> CreateChat(string Username, string ChatName, string Bio, byte[] ProfilePicture)
        {
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                ChatName = ChatName,
                Bio = Bio,
                // Add picture missing
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/create_chat", content))
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
                // Add picture missing
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/Chat/update_chat", content))
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/delete_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/Chat/add_user_chat", content))
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/Chat/rem_user_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<List<MessageDTO>> GetMessages(string Username, int Id)
        {
            List<MessageDTO> messageDTOs = new List<MessageDTO>();
            ChatDTO chatDTO = new ChatDTO
            {
                Username = Username,
                Id = Id
            };
            string jsonChatDTO = Newtonsoft.Json.JsonConvert.SerializeObject(chatDTO);
            StringContent content = new StringContent(jsonChatDTO, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/get_msg_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                messageDTOs = JsonConvert.DeserializeObject<List<MessageDTO>>(json);
                return messageDTOs;
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Chat/snd_msg_chat", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
    }
}
