using BPR_Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class UserService
    {
        private static readonly string url = "http://localhost:8080";

        public static async Task<string> Login(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/login", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<string>(json);
                    return token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> Register(string username, string password)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/register", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var str = JsonConvert.DeserializeObject<string>(json);
                    return str;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
