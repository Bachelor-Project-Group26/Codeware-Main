using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class PostService : IPostService
    {
         private string URL = "https://localhost:7000";
        // private string URL = "https://codeware-backend-bpr.azurewebsites.net/";
        public async Task<string> CreatePost(string username, string title,string postContent)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = 0,
                isUser = true,
                Title = title,
                Content = postContent,
                followedId = 0,
                Reaction = 0,
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/create_post", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> DeletePost(string username, int id)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = id,
                Content = "",
                isUser = true,
                Title = "",
                followedId = 0,
                Reaction = 0,
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
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

        public async Task<string> ReactToPost(string username, int id, int reaction)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = id,
                Content = "",
                isUser = true,
                Title = "",
                followedId = 0,
                CreatedDate = DateTime.Now,
                Reaction = reaction,
                Likes = 0,
                Comments = new List<CommentDTO>()
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

        public async Task<PostDTO> GetPost(string username, int id)
        {
            PostDTO post = new PostDTO();
            PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = id,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Likes = 0,
                Comments = new List<CommentDTO>()
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_post", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostDTO>(json);
                return post;
            }
        }

        public async Task<List<PostDTO>> GetPostListFromUser(string username, string usernameToGet)
        {
            List<PostDTO> posts = new List<PostDTO>();
            PostDTO post = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = 0,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = usernameToGet,
                Content = "",
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_post_list", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                posts = JsonConvert.DeserializeObject<List<PostDTO>>(json);
                return posts;
            }
        }
        public async Task<List<PostDTO>> GetPostList(string username)
        {
            List<PostDTO> posts = new List<PostDTO>();
            PostDTO post = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = 0,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_all_posts", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                posts = JsonConvert.DeserializeObject<List<PostDTO>>(json);
                return posts;
            }
        }
        public async Task<List<PostDTO>> GetPostListFromFollowing(string username)
        {
            List<PostDTO> posts = new List<PostDTO>();
            PostDTO post = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = 0,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_followed_posts", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                posts = JsonConvert.DeserializeObject<List<PostDTO>>(json);
                return posts;
            }
        }

        public async Task<string> Follow(string username, string usernameToFollow)
        {
            PostDTO post = new PostDTO
            {
                Username = username,
                Username2 = usernameToFollow,
                Id = 0,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()

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

        public async Task<string> Unfollow(string username,string usernameToUnfollow)
        {
            PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = usernameToUnfollow,
                Id = 0,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/Post/unfollow", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

       public async Task<string> Comment (string username, string Title, string Content, int id){
            PostDTO comment = new PostDTO{
                Username = username,
                Username2 = "",
                Id = id,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = Title,
                Content = Content,
                CreatedDate = DateTime.Now,
                Comments = new List<CommentDTO>()
                };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(comment);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/send_Comment", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
        public async Task<List<CommentDTO>> GetComments (string username, int id){
             List<CommentDTO> comments = new List<CommentDTO>();
             PostDTO user = new PostDTO
            {
                Username = username,
                Username2 = "",
                Id = id,
                followedId = 0,
                isUser = true,
                Reaction = 0,
                Title = "",
                Content = "",
                CreatedDate = DateTime.Now,
                Likes = 0,
                Comments = new List<CommentDTO>()
            };
            string jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/Post/get_Comments", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                comments = JsonConvert.DeserializeObject<List<CommentDTO>>(json);
                return comments;
            }
        }
    }
}
