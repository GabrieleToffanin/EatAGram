using Eatagram.SDK.Configuration.Authentication;
using Eatagram.SDK.Models.Authentication;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Services
{
    public class AuthenticationProvider
    {
        private IPublicClientApplication IdentityClient { get; init; }

        public AuthenticationProvider()
        {
            IdentityClient = PublicClientApplicationBuilder.Create(AuthenticationConstants.ApplicationId)
                                                           .WithAuthority("https://login.microsoftonline.com/66769b82-5df6-4623-997c-5ab2ab22b900")
                                                           .WithRedirectUri("http://localhost")
                                                           .Build();
        }

        public async Task<AuthenticationToken> AuthenticateUser()
        {
            var currentAccounts = await IdentityClient.GetAccountsAsync();
            AuthenticationResult result = null!;

            try
            {
                result = await IdentityClient
                    .AcquireTokenSilent(AuthenticationConstants.Scopes, currentAccounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                result = await IdentityClient.AcquireTokenInteractive(AuthenticationConstants.Scopes)
                                             .ExecuteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: Authentication failed: {ex.Message}");
            }

            return new AuthenticationToken
            {
                DisplayName = result?.Account?.Username ?? "",
                ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
                Token = result?.AccessToken ?? "",
                UserId = result?.Account?.Username ?? ""
            };
        }
    }
}
