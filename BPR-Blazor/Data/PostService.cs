using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class PostService : IPostService
    {
        private string URL = "https://localhost:7000";
        public async Task<string> CreatePost(string username, string postContent)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Content = postContent,
                followedId = "",
                CreatedDate = DateTime.Now
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/create_post", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.Write(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                Console.Write(str);
                return response.StatusCode + str;
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/delete_post", content))
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
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/react_post", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return str;
            }
        }

        public async Task<string> GetPost(string username, int id)
        {
            return "{\"Id\":1,\"Creator\":\"Bernardo\",\"FollowedId\":1,\"Content\":\"Testing\",\"CreatedDate\": \"2022-11-10T15:43:07.120Z\"}";
            //PostDTO post = new PostDTO
            //{
            //    Username = username,
            //    Id = id
            //    
            //};
            //string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            //StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            //using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_post", content))
            //{
            //    var json = await response.Content.ReadAsStringAsync();
            //    post = JsonConvert.DeserializeObject<PostDTO>(json);
            //    return post;
            //}
        }

        public async Task<string> GetPostList(string username)
        {
            return "{\"PostList\":[" +
                "{\"Id\":1,\"Creator\":\"Bernardo\",\"FollowedId\":1,\"Content\":\"Testing\",\"CreatedDate\": \"2022-11-10T15:43:07.120Z\"}," +
                "{\"Id\":2,\"Creator\":\"Bernardo\",\"FollowedId\":2,\"Content\":\"Testing\",\"CreatedDate\": \"2022-11-10T15:43:07.120Z\"}" +
                "]}";
            //PostDTO post = new PostDTO
            //{
            //    Username = username
            //};
            //string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            //StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            //using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_post_list", content))
            //{
            //    var json = await response.Content.ReadAsStringAsync();
            //    var str = JsonConvert.DeserializeObject<string>(json);
            //    return str;
            //}
        }

        public async Task<string> Follow(string username)
        {
            PostDTO post = new PostDTO
            {
                Username = username,
                Content = "",
                followedId = ""
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/follow", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> Unfollow(string username)
        {
            PostDTO user = new PostDTO
            {
                Username = username
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/unfollow", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
    }
}
