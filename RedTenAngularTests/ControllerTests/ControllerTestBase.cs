using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RedTenAngularTests.ControllerTests
{
    public class LoginResponse 
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
    public class ControllerTestBase
    {
        string JWT = string.Empty;
        readonly HttpClient http = new HttpClient();

        [OneTimeSetUp]
        public void Init()
        {
            http.BaseAddress = new Uri("https://localhost:44350/");
        }
        protected async Task LoginAsync()
        {
         
            var dict = new Dictionary<string, string>();
            dict.Add("username", "admin");
            dict.Add("password", "tempP@ss123");
            dict.Add("client_id", "quickapp_spa");
            dict.Add("grant_type", "password");
            dict.Add("scope", "openid email phone profile offline_access roles quickapp_api");
            using (var content = new FormUrlEncodedContent(dict))
            {
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var response = await http.PostAsync("connect/token", content);
                Assert.IsTrue(response.IsSuccessStatusCode);
                string loginResponseString = await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse = loginResponseString.To<LoginResponse>();
                JWT = loginResponse.access_token;
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWT);
            }

        }
        
        public async Task<T> PostAsync<T>(string path, object  body, bool successExpected = true) 
        {
            await LoginAsync();
            
            var response = await  http.PostAsync(path, new StringContent(body.ToJson()));
            Assert.AreEqual(successExpected, response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json.To<T>();
            }
            return default(T);
        }

        public async Task<T> GetAsync<T>(string path, Dictionary<string, string> queryParams=null, bool successExpected = true)
        {
            await LoginAsync();

            if (queryParams != null && queryParams.Any())
            {
                var key = queryParams.Keys.First();
                string query = $"?{key}={HttpUtility.UrlEncode(queryParams[key])}";
                for (int i=1; i<queryParams.Keys.Count(); i++)
                {
                    key = queryParams.Keys.ElementAt(i);
                    query += $"&{key}={HttpUtility.UrlEncode(queryParams[key])}";
                }
                path += query;
            }
            var response = await http.GetAsync(path);
            Assert.AreEqual(successExpected, response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json.To<T>();
            }
            return default(T);
        }
    }
}
