namespace WebClient.Helpers.Api
{
    public interface IApiCaller
    {
        Task<T> GetTAsync<T>(HttpMethod method, string url, string accessToken);
    }
}