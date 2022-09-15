namespace BPR_Blazor.Data
{
    public interface IChatService
    {
        public Task<string> GetChat();
        public Task<string> GetChatList();
        public Task<string> CreateChat();
        public Task<string> UpdateChat();
        public Task<string> DeleteChat();

        public Task<string> AddUsers();
        public Task<string> RemoveUsers();

        public Task<string> GetMessages();
        public Task<string> SendMessage();
        public Task<string> DeleteMessages();
    }
}
