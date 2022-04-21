using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Helpers.Api
{
    public class ApiCaller<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ApiCaller<T>> _logger;

        public ApiCaller(IHttpClientFactory httpClientFactory, ILogger<ApiCaller<T>> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T> GetTAsync(HttpMethod method, string url, string accessToken)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(method, url);

                httpRequestMessage.Headers.Add("Accept", "application/json");
                httpRequestMessage.Headers.Add("Content-Type", "application/json");

                var httpclient = _httpClientFactory.CreateClient();
                
                httpclient.DefaultRequestHeaders.Authorization = !String.IsNullOrEmpty(accessToken) ? null : new AuthenticationHeaderValue("Bearer", accessToken); //No access token authenticationheader = null

                var response = await httpclient.SendAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    using (var content = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(content);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return null;
        }
    }
}
