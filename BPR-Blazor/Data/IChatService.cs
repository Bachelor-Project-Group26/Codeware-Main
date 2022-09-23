﻿namespace BPR_Blazor.Data
{
    public interface IChatService
    {
        public Task<string> GetChat(string Username, int Id);
        public Task<string> GetChatList(string Username);
        public Task<string> CreateChat(string Username, string ChatName, string Bio, byte[] ProfilePicture);
        public Task<string> UpdateChat(string Username, string ChatName, string Bio, byte[] ProfilePicture);
        public Task<string> DeleteChat(string Username, int Id);

        public Task<string> AddUsers(string Username, string Username2, int Id);
        public Task<string> RemoveUsers(string Username, string Username2, int Id);

        public Task<string> GetMessages(string Username, int Id);
        public Task<string> SendMessage(string Username, int Id, string Content);
    }
}
