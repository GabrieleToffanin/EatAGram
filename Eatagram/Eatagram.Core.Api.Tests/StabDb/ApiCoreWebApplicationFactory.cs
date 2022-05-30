using Eatagram.Core.Data.EntityFramework.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests.StabDb
{
    public class ApiCoreWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : ControllerBase
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var currentDbDescriptor = services.SingleOrDefault(
                    x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(currentDbDescriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                services.AddEntityFrameworkInMemoryDatabase();

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                Utilities.IntializeDbForTest(db);


            });
        }
    }
}
