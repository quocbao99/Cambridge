using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZoomAPI.Configuaration;
using ZoomAPI.Models.Reponse;
using ZoomAPI.Models.Request;

namespace ZoomLibary
{
    public class ZoomClient
    {
        private HttpClient _client;
        public ZoomClient() {
            CreateHttpClient();
        }
        public async Task<AuthorizationResponseData> GetAuthorizationRequest()
        {
            EnsureHttpClientCreated();

            var byteArray = Encoding.ASCII.GetBytes($"{ConfigHelper.ClientId}:{ConfigHelper.ClientSecret}");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var keyValueParis = new List<KeyValuePair<string, string>>
                { 
                //new KeyValuePair<string, string>("code", "account_credentials"),
                new KeyValuePair<string, string>("account_id", ConfigHelper.AccountId),
                new KeyValuePair<string, string>("grant_type", "account_credentials"),
                //new KeyValuePair<string, string>("redirect_uri", "account_credentials"),
                //new KeyValuePair<string, string>("code_verifier", "account_credentials"),
                //new KeyValuePair<string, string>("device_code", "account_credentials"),
            };

            var response = await _client.PostAsync($"{ConfigHelper.BaseUrl}/oauth/token", new FormUrlEncodedContent(keyValueParis));

            var responseAsString = await response.Content.ReadAsStringAsync();

            var authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponseData>(responseAsString);

            return authorizationResponse;
        }
        public async Task<CreateMeetingReponse> CreateMeeting(CreateMeetingRequest request)
        {
            EnsureHttpClientCreated();

            var requestContent = JsonConvert.SerializeObject(request);

            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.PostAsync($"{ConfigHelper.BaseUrl}/v2/users/me/meetings", httpRequestMessage.Content);

            var responseAsString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateMeetingReponse>(responseAsString);

            return result;
        }
        private void CreateHttpClient()
        {
            _client = new HttpClient();
        }
        public void SetToken(string token)
        {
            _client.SetBearerToken(token);
        }
        private void EnsureHttpClientCreated()
        {
            if (_client == null)
            {
                CreateHttpClient();
            }
        }
    }
}
