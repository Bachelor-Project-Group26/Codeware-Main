using BPR_Blazor.Models;
using Newtonsoft.Json;

namespace BPR_Blazor.Data
{
    public class UserService
    {
        private static readonly string url = "http://localhost:8080";

        public static async Task<User> Login(string username, string password)
        {
            return new User(username, 1, "Bernardo", "b@gmail.com", new DateTime(2000, 12, 30), null);
            //using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{url}/user?username={username}&password={password}"))
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var json = await response.Content.ReadAsStringAsync();
            //        //var user = JsonConvert.DeserializeObject<User>(json);
            //        var user = new User(username, 1, "a", "a", new DateTime(100), null);
            //        return user;
            //    }
            //    else
            //    {
            //        throw new Exception(response.ReasonPhrase);
            //    }
            //}
        }

        public static async Task<bool> Register(string username, string password)
        {
            return true;
            //User user = new User(0, username, password, 0);
            //string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            //using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/user", content))
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var json = await response.Content.ReadAsStringAsync();
            //        var boolean = JsonConvert.DeserializeObject<bool>(json);
            //        return boolean;
            //    }
            //    else
            //    {
            //        throw new Exception(response.ReasonPhrase);
            //    }
            //}
        }
    }
}
