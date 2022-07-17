using Eatagram.SDK.Models;
using Eatagram.SDK.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Interfaces
{
    public interface IEatagramRecipesService
    {
        Task<HttpResponseMessage<IEnumerable<RecipeContract>>> FetchRecipes();
    }
}
