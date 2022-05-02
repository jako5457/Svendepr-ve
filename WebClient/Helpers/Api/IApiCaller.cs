namespace WebClient.Helpers.Api
{
    public interface IApiCaller
    {
        Task<T> GetTAsync<T>( string url, string accessToken);

        Task<bool> PostAsync<T>(string url, string accessToken, T Input);

        Task PutAsync(string url, string accessToken, string id);

        Task<bool> DeleteAsync(string url, string accessToken, string id);
    }
}