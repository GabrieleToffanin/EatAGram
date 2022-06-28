using Eatagram.Core.Api.Tests.Fixtures.Common;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests.Fixtures
{
    public sealed class CommentTestsFixture<TStartup> : TestsBase<Program>
    {
        private protected override void ConfigureDb(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CommentTestDb"));
        }
    }
}
