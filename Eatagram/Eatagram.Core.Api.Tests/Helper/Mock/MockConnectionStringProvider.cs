using Eatagram.Core.Interfaces.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests.Helper.Mock
{
    internal class MockConnectionStringProvider : IConnectionStringsProvider
    {
        public async ValueTask<string> GetMongoConnectionString()
        {
            return "Not now";
        }

        public async ValueTask<string> GetSqlConnectionString()
        {
            return string.Empty;
        }
    }
}
