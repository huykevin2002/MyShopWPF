using MyShop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Service
{
    class OrderService
    {
        protected static string resourceUrl = "http://localhost:8080/api/orders";

        public static async Task<ObservableCollection<Order>> Query(int page)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}?page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Order>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Order>> QueryLatestOrdersInThisWeek()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/by-desc-last-modified-date-in-this-week?size=5");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Order>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Order>> QueryLatestOrdersInThisMonth()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/by-desc-last-modified-date-in-this-month?size=5");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Order>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<Order>> QueryWithinDate(int page, string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date?dateFrom={dateFrom}&dateTo={dateTo}&page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<Order>>(responseBody);
            }
        }

        public static async Task<long> CountLatestOrdersInThisMonth()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/count/by-desc-last-modified-date-in-this-month");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }

        public static async Task<long> CountLatestOrdersInThisWeek()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/count/by-desc-last-modified-date-in-this-week");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }

        public static async Task<Order> Create(Object order)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(order);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync(resourceUrl, requestContent);

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Order>(responseBody);
            }
        }

        public static async Task<bool> CreateMultiple(List<Object> orders)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(orders);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync($"{resourceUrl}/multiple", requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Update(int id, Object order)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(order);
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
