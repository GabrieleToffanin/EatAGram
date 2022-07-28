
using Eatagram.AzureAuth.Cli.Auth;
using Microsoft.Identity.Client;



const string graphApiEndpoint = "https://graph.microsoft.com/v1.0/me";


var authService = new AuthService();

var authResult = await authService.PublicClientApp.AcquireTokenForClient(new string[] { "api://3ac89266-35e1-4b21-93f2-79626ec7f2ed/.default" }).ExecuteAsync();

Console.WriteLine(authResult.AccessToken);



    