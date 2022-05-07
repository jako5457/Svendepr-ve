﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace WebClient.Helpers.Api
{
    public class ApiCaller : IApiCaller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiCaller(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        private Tuple<HttpClient, HttpRequestMessage> SetupClient(HttpMethod httpMethod, string url, string accessToken)
        {
            url = _configuration.GetValue<string>("Api:Endpoint") + url;
            var httpRequestMessage = new HttpRequestMessage(httpMethod, url);
            //httpRequestMessage.Headers.Add("Accept", "application/json");
            var httpclient = _httpClientFactory.CreateClient();
            httpclient.DefaultRequestHeaders.Authorization = String.IsNullOrEmpty(accessToken) ? null : new AuthenticationHeaderValue("Bearer", accessToken); //No access token authenticationheader = null
            return Tuple.Create(httpclient, httpRequestMessage);
        }

        public async Task<bool> PostAsync<T>(string url, string accessToken, T Input)
        {
            try
            {
                var setup = SetupClient(HttpMethod.Post, url, accessToken);

                HttpContent content = new StringContent(JsonSerializer.Serialize<T>(Input), Encoding.UTF8, Application.Json);

                HttpResponseMessage response = await setup.Item1.PostAsJsonAsync<T>(setup.Item2.RequestUri, Input );

                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task PutAsync(string url, string accessToken, string id)
        {
            try
            {
                var setup = SetupClient(HttpMethod.Put, url + "/" + id, accessToken);

                var response = await setup.Item1.SendAsync(setup.Item2);

                if (response.IsSuccessStatusCode)
                {
                    using var content = await response.Content.ReadAsStreamAsync();
                    //return await JsonSerializer.DeserializeAsync<T>(content);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(string url, string accessToken, string id)
        {
            try
            {
                var setup = SetupClient(HttpMethod.Delete, url , accessToken);

                HttpResponseMessage response = await setup.Item1.DeleteAsync(setup.Item2.RequestUri);

                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> GetTAsync<T>(string url, string accessToken)
        {
            try
            {
                var setup = SetupClient(HttpMethod.Get, url, accessToken);

                //var setup = SetupClient(HttpMethod.Get, url, accessToken);

                var response = await setup.Item1.SendAsync(setup.Item2);

                using var ContentStream = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<T>(ContentStream);
            }
            catch (Exception)
            {
                return default;
            }

            return default;
        }
    }
}