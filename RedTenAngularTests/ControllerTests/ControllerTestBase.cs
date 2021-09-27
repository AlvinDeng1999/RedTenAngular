using DAL.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RedTenAngular;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Insight.Database;

namespace RedTenAngularTests.ControllerTests
{
    public class TestSettings
    {
        public string WebAppRootUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
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
        readonly HttpClient _http = new HttpClient();
        readonly TestSettings _testSettings;

        protected Group _testCreatedGroup;
        readonly protected string _databaseCoonnection;

        public ControllerTestBase()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            _testSettings = configuration.GetSection("AppSettings").Get<TestSettings>();
            _databaseCoonnection = configuration.GetConnectionString("DefaultConnection");
        }

        [OneTimeSetUp]
        public async Task Init()
        {      
            _http.BaseAddress = new Uri(_testSettings.WebAppRootUrl);
            await LoginAsync();

            Group group = new Group()
            {
                Name = "TestGroup"
            };
            this._testCreatedGroup = await PostAsync<Group>("api/Groups", group);
        }
        [OneTimeTearDown]
        public async Task Finalize()
        {
            if (_testCreatedGroup != null)
            {
                using (var conn = new SqlConnection(_databaseCoonnection))
                {
                    await conn.ExecuteSqlAsync("delete from groups where id=@id", new { id = _testCreatedGroup.id });
                }
            }
        }
        protected async Task LoginAsync()
        {
         
            var dict = new Dictionary<string, string>();
            dict.Add("username", _testSettings.UserName);
            dict.Add("password", _testSettings.Password);
            dict.Add("client_id", "quickapp_spa");
            dict.Add("grant_type", "password");
            dict.Add("scope", "openid email phone profile offline_access roles quickapp_api");
            using (var content = new FormUrlEncodedContent(dict))
            {
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var response = await _http.PostAsync("connect/token", content);
                Assert.IsTrue(response.IsSuccessStatusCode);
                string loginResponseString = await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse = loginResponseString.To<LoginResponse>();
                JWT = loginResponse.access_token;
            }

        }
        
        public async Task<T> PostAsync<T>(string path, object body, bool successExpected = true) 
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {JWT}");
            
            HttpContent content = new StringContent(body.ToJson(), Encoding.UTF8, "application/json");
            var response = await  _http.PostAsync(path, content);
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            Assert.AreEqual(successExpected, response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                return json.To<T>();
            }
            return default(T);
        }

        public async Task<T> GetAsync<T>(string path, Dictionary<string, string> queryParams=null, bool successExpected = true)
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {JWT}");

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
            var response = await _http.GetAsync(path);
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            Assert.AreEqual(successExpected, response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                
                return json.To<T>();
            }
            return default(T);
        }
    }
}
