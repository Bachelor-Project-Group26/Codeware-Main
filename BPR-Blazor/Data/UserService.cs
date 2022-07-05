using BPR_Blazor.Models;
using Newtonsoft.Json;
using System.Text;

namespace BPR_Blazor.Data
{
    public class UserService
    {
        private static readonly string url = "http://localhost:8080";

        public static async Task<User> Login(string username, string password)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{url}/user?username={username}&password={password}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(json);
                    return user;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> Register(string username, string password, int securityLevel, string name, string email, DateTime birthday)
        {
            UserWithPassword userWithPassword = new UserWithPassword(username, password, securityLevel, name, email, birthday);
            string jsonUserWithPassword = Newtonsoft.Json.JsonConvert.SerializeObject(userWithPassword);
            StringContent content = new StringContent(jsonUserWithPassword, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/user", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var integer = JsonConvert.DeserializeObject<int>(json);
                    switch(integer)
                    {
                        case 201: // Created
                            return "User created!";
                        case 400: // Bad request
                            return "User already exits!";
                        default:
                            return "There was an issue in the server!";
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
