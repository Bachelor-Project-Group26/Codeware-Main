using BPR_Blazor.Models;
namespace BPR_Blazor.Data
{
    public interface IPostService
    {
        public Task<string> CreatePost(string username, string title,string postContent);
        public Task<string> DeletePost(string username, int id);
        public Task<string> ReactToPost(string username, int reaction);
        public Task<PostDTO> GetPost(string username, int id);
        public  Task<List<PostDTO>> GetPostList(string username);
        public Task<string> Follow(string username);
        public Task<string> Unfollow(string username);
    }
}