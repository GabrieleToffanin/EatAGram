using Eatagram.SDK.Models.Authentication;
using Eatagram.SDK.Services;

//AuthenticationProvider auth = new AuthenticationProvider();

//var user = await auth.AuthenticateUser();



//Console.WriteLine(user.Token);

//Console.ReadLine();

AuthenticationProvider auth = new AuthenticationProvider();

var token = await auth.AuthenticateUser();

EatagramRecipesService recipeService = new EatagramRecipesService(token);

var result = await recipeService.FetchRecipes();

foreach(var item in result.Content)
    Console.WriteLine(item.Name);