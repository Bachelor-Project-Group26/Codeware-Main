﻿using BPR_Blazor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BPR_Blazor.Data
{
    public class UserService : IUserService
    {
        private string url = "http://localhost:7000";


        public async Task<string> Login(string username, string password)
        {
            UserDTO user = new UserDTO {
                Username = username,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
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
            UserDTO user = new UserDTO
            {
                Username = username,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync($"{url}/User/register", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> UpdateDetails(string username, string token, int securityLevel, string name, string email, DateTime birthday)
        {
            UserDTO user = new UserDTO
            {
                Username = username,
                Token = token,
                SecurityLevel = securityLevel,
                Name = name,
                Email = email,
                Birthday = birthday
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{url}/User/update_details", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }

        public async Task<string> UpdatePassword(string username, string token, string password)
        {
            UserDTO user = new UserDTO
            {
                Username = username,
                Token = token,
                Password = password
            };
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync($"{url}/User/delete", content))
            {
                var json = await response.Content.ReadAsStringAsync();
                var str = JsonConvert.DeserializeObject<string>(json);
                return response.StatusCode + str;
            }
        }


        public async Task<string> DeleteUser(string username)
        {
            return "";
        }
    }
}
