using System.Security.Claims;

namespace WebClient.Helpers.UserHelpers
{
    public static class ClaimHelper
    {
        public static string GetValueFromClaim(this IEnumerable<Claim> claims, string typevalue)
        {
            try
            {
                return claims.FirstOrDefault(c => c.Type == typevalue).Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
