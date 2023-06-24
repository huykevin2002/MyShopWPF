using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyShop.Model;
using System.Runtime.CompilerServices;

namespace MyShop.Service
{
    public class LoginService
    {
        protected static string resourceUrl = "http://localhost:8080/api/account";

        private static string _username;

        private static string _password;

        public static async Task<bool> Login(string username, string password)
        {
            _username = username ?? string.Empty;
            _password = password ?? string.Empty;
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync(resourceUrl);

                return response.IsSuccessStatusCode;
            }
        }

        public static string GetUsername() { return _username; }

        public static string GetPassword() { return _password; }

    }
}

