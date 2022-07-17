using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Mocks.Authentication;
using Eatagram.Core.Mocks.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eatagram.Core.Api.Tests.Fixtures.Common
{
    public abstract class TestsBase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(serv => serv.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(serviceDescriptor!);

                ConfigureDb(services);

                services.AddEntityFrameworkInMemoryDatabase();

                services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                var build = services.BuildServiceProvider();

                using var scoped = build.CreateScope();

                var current = scoped.ServiceProvider;
                var db = current.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                FakeDataSeed.IntitDatabase(db);
            });
        }
        private protected abstract void ConfigureDb(IServiceCollection services);
    }
}
