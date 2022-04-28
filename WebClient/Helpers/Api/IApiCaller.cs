namespace WebClient.Helpers.Api
{
    public interface IApiCaller
    {
        Task<T> GetTAsync<T>( string url, string accessToken);

        Task PostAsync<T>(string url, string accessToken, T Input);

        Task PutAsync(string url, string accessToken, string id);

        Task DeleteAsync(string url, string accessToken, string id);
    }
}