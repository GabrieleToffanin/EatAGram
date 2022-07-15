using Eatagram.SDK.Services.AuthenticationService;

AuthenticationProvider auth = new AuthenticationProvider();

var user = await auth.AuthenticateUser();



Console.WriteLine(user.Token);

Console.ReadLine();