using Eatagram.Framework.Testability.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Framework.Testability.WebApplicationFactoryExtensions
{
    public static class WebApplicationFactoryExtension
    {
        public static WebApplicationFactory<T> WithAuthentication<T>(this WebApplicationFactory<T> factory) where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Configure<TestAuthenticationHandlerOptions>(options => options.DefaultUserId = "UserId");

                    services.AddAuthentication("Test")
                            .AddScheme<TestAuthenticationHandlerOptions, TestAuthenticationHandler>("Test", options => { });
                });
            });
        }

        public static HttpClient CreateClientWithTestAuth<T>(this WebApplicationFactory<T> factory) where T : class
        {
            var client = factory.WithAuthentication().CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            client.BaseAddress = new Uri("https://localhost/");

            return client;
        }
    }
}
