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
    class OrderItemService
    {
        protected static string resourceUrl = "http://localhost:8080/api/order-items";

        public static async Task<ObservableCollection<OrderItem>> Query(int page)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}?page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<OrderItem>>(responseBody);
            }
        }

        public static async Task<ObservableCollection<OrderItem>> QueryByOrderId(int orderId, int page)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/byOrderId/{orderId}?page={page}&size={Setting.GetItemsPerPage()}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<OrderItem>>(responseBody);
            }
        }

        public static async Task<bool> Create(Object orderItem)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(orderItem);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync(resourceUrl, requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> CreateMultiple(List<Object> orderItems)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(orderItems);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.PostAsync($"{resourceUrl}/multiple", requestContent);

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Update(int id, Object orderItem)
        {
            using (var client = new HttpClient())
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(orderItem);
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

        public static async Task<long> countAllByOrderId(int id)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/count/byOrderId/{id}");

                return long.Parse(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<long> totalPriceByOrderId(int id)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/totalPrice/{id}");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }

        public static async Task<long> totalRevenueInThisWeek()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/total-revenue-in-this-week");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }

        public static async Task<long> totalRevenueInThisMonth()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/total-revenue-in-this-month");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(string.Empty))
                {
                    return 0L;
                }
                return long.Parse(responseBody);
            }
        }

        public static async Task<List<string>> WithinDateRevenueLabels(string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date-revenue/labels?dateFrom={dateFrom}&dateTo={dateTo}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> WithinDateRevenueValues(string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date-revenue/values?dateFrom={dateFrom}&dateTo={dateTo}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> WeeklyRevenueLabels()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/weekly-revenue/labels");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> WeeklyRevenueValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/weekly-revenue/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> MonthlyRevenueLabels()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/monthly-revenue/labels");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> MonthlyRevenueValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/monthly-revenue/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> YearlyRevenueLabels()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/yearly-revenue/labels");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> YearlyRevenueValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/yearly-revenue/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<long>> WeeklyProfitValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/weekly-profit/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<long>> MonthlyProfitValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/monthly-profit/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<long>> YearlyProfitValues()
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/yearly-profit/values");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<long>> WithinDateProfitValues(string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date-profit/values?dateFrom={dateFrom}&dateTo={dateTo}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> WithinDateStockLabels(long bookId, string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date-stock/labels?bookId={bookId}&dateFrom={dateFrom}&dateTo={dateTo}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> WithinDateStockValues(long bookId, string dateFrom, string dateTo)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/within-date-stock/values?bookId={bookId}&dateFrom={dateFrom}&dateTo={dateTo}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> WeeklyStockLabels(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/weekly-stock/labels?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> WeeklyStockValues(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/weekly-stock/values?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> MonthlyStockLabels(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/monthly-stock/labels?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> MonthlyStockValues(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/monthly-stock/values?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }

        public static async Task<List<string>> YearlyStockLabels(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/yearly-stock/labels?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
        }

        public static async Task<List<long>> YearlyStockValues(long bookId)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{LoginService.GetUsername()}:{LoginService.GetPassword()}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync($"{resourceUrl}/yearly-stock/values?bookId={bookId}");
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<long>>(responseBody);
            }
        }
    }
}
