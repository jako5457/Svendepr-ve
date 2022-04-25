using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace XamarinClient.Helpers.UserHelpers
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
