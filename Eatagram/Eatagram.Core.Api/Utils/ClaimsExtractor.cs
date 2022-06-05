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
            => claims.Claims.Where(x => x.Type == "UserId").FirstOrDefault()!.Value;
    }
}
