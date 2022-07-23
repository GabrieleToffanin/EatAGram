using Eatagram.Core.Api.Tests.Fixtures.Common;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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
