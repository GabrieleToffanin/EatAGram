using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Configuration.Authentication
{
    internal static class AuthenticationConstants
    {
        internal readonly static string ApplicationId = "d182fc04-0480-4c04-9f22-56391ce56723";
        internal readonly static string Tenant = "66769b82-5df6-4623-997c-5ab2ab22b900";
        internal readonly static string[] Scopes = new[]
        {
            "api://3ac89266-35e1-4b21-93f2-79626ec7f2ed/api.consume",
            "api://3ac89266-35e1-4b21-93f2-79626ec7f2ed/access_as_user"
        };
    }
}
