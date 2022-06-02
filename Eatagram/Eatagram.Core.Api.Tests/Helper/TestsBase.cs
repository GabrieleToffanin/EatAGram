using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Data.EntityFramework.Repository;
using Eatagram.Core.Entities;
using Eatagram.Core.Logic;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests.Helper
{
    public class TestsBase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var build = services.BuildServiceProvider();

                using var scoped = build.CreateScope();

                var current = scoped.ServiceProvider;
                var db = current.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
            });
        }
    }
}
