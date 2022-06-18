using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Eatagram.Core.Api.Tests.Helper
{
    public sealed class TestsBase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {


            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(serv => serv.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(serviceDescriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));

                services.AddEntityFrameworkInMemoryDatabase();


                var build = services.BuildServiceProvider();

                using var scoped = build.CreateScope();

                var current = scoped.ServiceProvider;
                var db = current.GetRequiredService<ApplicationDbContext>();
                var userManager = current.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = current.GetRequiredService<RoleManager<IdentityRole>>();

                db.Database.EnsureCreated();

                Utilities.InitIdentityDb(db, userManager, roleManager);
            });

        }

        protected override void ConfigureClient(HttpClient client)

        {
            base.ConfigureClient(client);
            string token = SetAuthHeader(client).Result;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<string> SetAuthHeader(HttpClient client)
        {
            var user = new JwtTokenRequest
            {
                Password = "Unicorn-12",
                Username = "GT@outlook.it"
            };

            var request = await client.PostAsJsonAsync("api/Authentication/Authenticate", user);
            var content = await request.Content.ReadAsStringAsync();

            var wanted = JsonConvert.DeserializeObject<TokenExtractor>(content);


            return wanted.Token;
        }


    }
}
