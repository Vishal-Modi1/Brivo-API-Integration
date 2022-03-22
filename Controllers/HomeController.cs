using BrivoAPIIntegration.Configuration;
using BrivoAPIIntegration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using BrivoAPI.Models;
using Brivo_API.Models;

namespace BrivoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ConfigurationSettings _configurationSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginResponse _loginResponse;

        public HomeController()
        {
            _configurationSettings = ConfigurationSettings.Instance;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
            try
            {
                List<KeyValuePair<string, string>> loginInfo = new List<KeyValuePair<string, string>>();

                loginInfo.Add(new KeyValuePair<string, string>("grant_type", "password"));
                loginInfo.Add(new KeyValuePair<string, string>("username", loginModel.UserName));
                loginInfo.Add(new KeyValuePair<string, string>("password", loginModel.Password));

                using (var client = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(loginInfo))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"Basic { _configurationSettings.AuthorizationKey}");

                        HttpResponseMessage httpResponseMessage = await client.PostAsync(_configurationSettings.APITokenURL, content);

                        string data = httpResponseMessage.Content.ReadAsStringAsync().Result;

                        _loginResponse = JsonConvert.DeserializeObject<LoginResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                        return Ok(_loginResponse);

                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [HttpPut]
        [Route("UpdateUserSuspendStatus")]
        public async Task<IActionResult> UpdateUserSuspendStatus(long id, string token, bool status)
        {
            try
            {
                string url = $"{ _configurationSettings.APIURL}users/{id}/suspended";

                using (var client = new HttpClient())
                {
            
                    client.DefaultRequestHeaders.Add("api-key", _configurationSettings.APIKey);
                    client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {token}");

                    SuspendStatusRequest suspendStatus = new SuspendStatusRequest();
                    suspendStatus.suspended = status;

                    string json = JsonConvert.SerializeObject(suspendStatus);

                    StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                    HttpResponseMessage httpResponseMessage = await client.PutAsync(url, stringContent);

                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    SuspendStatusResponse suspendStatusResponse = JsonConvert.DeserializeObject<SuspendStatusResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                    return Ok(suspendStatusResponse);

                }
            }
            catch (Exception exc)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("ActivateAccessPoint")]
        public async Task<IActionResult> UpdateUserSuspendStatus(long id, string token)
        {
            try
            {
                string url = $"{ _configurationSettings.APIURL}access-points/{id}/activate";

                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("api-key", _configurationSettings.APIKey);
                    client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {token}");

                    SuspendStatusRequest suspendStatus = new SuspendStatusRequest();
               //     suspendStatus.suspended = status;

                    string json = JsonConvert.SerializeObject(suspendStatus);

                    StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); 

                    HttpResponseMessage httpResponseMessage = await client.PutAsync(url, stringContent);

                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    SuspendStatusResponse suspendStatusResponse = JsonConvert.DeserializeObject<SuspendStatusResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                    return Ok(suspendStatusResponse);

                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
