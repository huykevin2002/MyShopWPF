using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyShop.Model;

namespace MyShop.Service
{
    class BookService
    {
        protected static string resourceUrl = "http://localhost:8080/api/books";

        public static async Task<ObservableCollection<Book>> Query(int page)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}?page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Book>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Book>> Search(int page, string key, string classificationId, string minPrice, string maxPrice)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}?title.contains={key}&classificationId.equals={classificationId}&sellingPrice.greaterThanOrEqual={minPrice}&sellingPrice.lessThanOrEqual={maxPrice}&page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Book>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Book>> QueryAll()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/all");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Book>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Book>> QueryLeastQuantity()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/by-asc-quantity?size=5");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Book>>(responseBody);
            }
        }

        public static async Task<bool> Create(Object book)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(book);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync(resourceUrl, requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> CreateMultiple(List<Object> books)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(books);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync($"{resourceUrl}/multiple", requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Update(int id, Object book)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(book);
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
