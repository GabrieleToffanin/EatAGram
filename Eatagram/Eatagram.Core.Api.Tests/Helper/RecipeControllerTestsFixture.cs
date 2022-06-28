using Eatagram.Core.Api.Tests.Helper.Mock;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Token;
using Eatagram.Core.Interfaces.Azure;
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
    public class RecipeControllerTestsFixture<TStartup> : TestsBase<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(serv => serv.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(serviceDescriptor);
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("RecipeControllerTests"));
            });
            base.ConfigureWebHost(builder);
        }
    }
}
