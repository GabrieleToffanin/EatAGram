using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Eatagram.Core.Api.Utils
{
    public static class ClaimsExtractor
    {
        /// <summary>
        /// Gets the current user Id for the authenticated user, 
        /// necessary for the creation of a new recipe
        /// </summary>
        /// <param name="claims">Claims owned by the logged user</param>
        /// <returns>The string value of the user id</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetUserId(this ClaimsPrincipal claims)
            => claims.Claims.Single(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "Test";
    }
}
