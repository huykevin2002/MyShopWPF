using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyShop.Model;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows.Media.Animation;

namespace MyShop.Service
{
    class ClassificationService
    {
        protected static string resourceUrl = "http://localhost:8080/api/classifications";

        public static async Task<ObservableCollection<Classification>> Query(int page)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}?page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Classification>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Classification>> QueryAll()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/all");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Classification>>(responseBody);
            }
        }

        public static async Task<bool> Create(Object classification)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(classification);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync(resourceUrl, requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> CreateMultiple(List<Object> classification)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(classification);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync($"{resourceUrl}/multiple", requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Update(int id, Object classification)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(classification);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PatchAsync($"{resourceUrl}/{id}", requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.DeleteAsync($"{resourceUrl}/{id}");

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<long> countAll()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/count");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }
    }
}
