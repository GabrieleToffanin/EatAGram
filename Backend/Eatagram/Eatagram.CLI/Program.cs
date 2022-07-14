using Eatagram.CLI.Auth;

AzureAuthentication auth = new AzureAuthentication();


string token = await auth.AuthenticateAsync();


Console.WriteLine(token);


