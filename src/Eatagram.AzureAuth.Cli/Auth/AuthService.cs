using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.AzureAuth.Cli.Auth
{
    public class AuthService
    {
        public AuthService()
        {
            PublicClientApp = ConfidentialClientApplicationBuilder.Create(ClientId)
                .WithClientSecret(ClientSecret)
                .WithAuthority("https://login.microsoftonline.com/"+ Tenant)
                .Build();
        }

        public IConfidentialClientApplication PublicClientApp { get; set; }

        private const string ClientId = "6b8c2033-1faf-45d6-a83a-d3820db138d7";
        private const string ClientSecret = "x.m8Q~WNc6BiEnF6NLI34Llj.IxsNx2~ezQV2cN_";
        private const string Tenant = "66769b82-5df6-4623-997c-5ab2ab22b900";
    }
}
