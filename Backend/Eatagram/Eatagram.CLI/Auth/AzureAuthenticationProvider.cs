using Eatagram.CLI.Constants;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.CLI.Auth
{
    public class AzureAuthentication
    {
        public async Task<string> AuthenticateAsync()
        {
            AuthenticationContext authContext = new AuthenticationContext(AppModeConstants.AuthString, false);

            ClientCredential creds = new ClientCredential(AppModeConstants.ClientId, AppModeConstants.ClientSecret);

            AuthenticationResult authResult = await authContext.AcquireTokenAsync(GlobalConstants.ResourceUrl, creds);

            return authResult.AccessToken;
        }
    }
}
