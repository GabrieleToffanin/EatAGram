﻿namespace Eatagram.SDK.Models.Requests
{
    public class RecipeCreationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientCreationRequest> Ingredients { get; set; }
    }
}