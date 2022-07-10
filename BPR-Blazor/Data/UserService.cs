using BPR_Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class UserService : IUserService
    {
        private static readonly string url = "https://localhost:7000";
        private ILocalStorageService _localStorageService;
        public UserWithPassword user { get; private set; }

        public UserService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> Login(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                if (!response.IsSuccessStatusCode)
                {
                    str = response.StatusCode + str;
                }
                
                await _localStorageService.SetItem("user", str);
                return str;
            }
        }

        public async Task<string> Register(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/register", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
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
