using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Azure
{
    public interface IConnectionStringsProvider
    {
        ValueTask<string> GetSqlConnectionString();
        ValueTask<string> GetMongoConnectionString();
    }
}
