using BPR_Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BPR_Blazor.Data
{
    public class UserService : IUserService
    {
        private static readonly string url = "https://localhost:7000";
        public UserWithPassword user { get; private set; }
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _client;
        
        public UserService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _client = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        
        
        public async Task<string> Login(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/User/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                if (!response.IsSuccessStatusCode)
                {
                    str = response.StatusCode + str;
                }
                return str;
            }
        }
        
        public async Task<string> Register(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/User/register", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }
        public async Task<string> UpdateUser(string username, int securityLevel, string name, string email, DateTime birthday)
        {
            UserDetails userDetails = new UserDetails(username, securityLevel, name, email, birthday);
            return "";
        }

        public async Task<string> DeleteUser(string username)
        {
            return "";
        }
    }
}
