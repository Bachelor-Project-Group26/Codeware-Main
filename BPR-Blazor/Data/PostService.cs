using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class PostService
    {
        private string URL = "https://localhost:7000";

        public async Task<string> CreatePost(string username, string postContent)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Content = postContent,
                CreatedDate = DateTime.Now
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<string> DeletePost(string username, int id)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Id = id
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<string> ReactToPost(string username, int reaction)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Reaction = reaction
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<string> GetPost(string username, int id)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Id = id
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<string> GetPostList(string username)
        {
            PostDTO user = new PostDTO
            {
                Username = username
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }
    }
}
