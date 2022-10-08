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
        
        private string URL = "https://localhost:7000";

        public async Task<string> Login(string username, string password)
        {
            UserDTO user = new UserDTO {
                Username = username,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/login", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                if (response.IsSuccessStatusCode) ApiHelper.AddToken(str);
                else str = response.StatusCode + str;
                return str;
            }
        }

        public void Logout()
        {
            ApiHelper.RemoveToken();
            Console.WriteLine("Logged out!");
        }

        public async Task<string> Register(string username, string password)
        {
            UserDTO user = new UserDTO
            {
                Username = username,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{URL}/User/register", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
        public async Task<UserDTO> GetUserByUsername(string username)
        {
            UserDTO user = new UserDTO();
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{URL}/User/{username}"))
            {
                var json = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserDTO>(json);
                return user;
            }
        }
        public async Task<string> UpdateDetails(string username, int securityLevel, string firstName, 
            string lastName, string email, string country, string bio, byte[] profilePicture, DateTime birthday)
        {
            UserDTO user = new UserDTO
            {
                Username = username,
                SecurityLevel = securityLevel,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Country = country,
                Bio = bio,
                // Add picture missing
                Birthday = birthday
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/update_details", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> UpdatePassword(string username, string password)
        {
            UserDTO user = new UserDTO
            {
                Username = username,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/delete", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        //Returns a list of IDs, Usernames, First Name, Last Name
        //public async Task<List<UserDTO>> GetAllUsers()
        //{
        //    UserDTO user = new UserDTO
        //    {
        //        Username = username
        //    };
        //    string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
        //    StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
        //    using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{url}/User/get_users", content))
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var str = JsonConvert.DeserializeObject<string>(json);
        //        return response.StatusCode + str;
        //    }
        //}

        public async Task<string> DeleteUser(string username)
        {
            UserDTO user = new UserDTO
            {
                Username = username
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{URL}/User/delete", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }
    }
}
