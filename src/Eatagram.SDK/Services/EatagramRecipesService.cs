using Eatagram.SDK.Interfaces;
using Eatagram.SDK.Models;
using Eatagram.SDK.Models.Authentication;
using Eatagram.SDK.Models.Contracts;
using Eatagram.SDK.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Services
{
    public sealed class EatagramRecipesService : HttpServiceBase, IEatagramRecipesService
    {
        public EatagramRecipesService(AuthenticationToken authUser) : base(authUser)
        {
        }

        public Task<HttpResponseMessage<IEnumerable<RecipeContract>?>> FetchRecipes()
        {
            return InvokeAsync<object, IEnumerable<RecipeContract>>(
                null, "api/Recipe/GetRecipes", HttpMethod.Get);
        }
    }
}
