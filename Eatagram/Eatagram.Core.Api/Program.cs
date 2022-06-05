using Eatagram.Core.Api.Config;
using Eatagram.Core.Api.Extensions;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Data.EntityFramework.Repository;
using Eatagram.Core.Entities.Token;
using Eatagram.Core.Logic;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public partial class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<JwtToken>(builder.Configuration.GetSection("token"));

        builder.Services.SetupIdentityDatabase(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        

        builder.Services.AddScoped<IRecipeRepository, RecipesRepository>();
        builder.Services.AddScoped<IRecipeBrainLogic, RecipeBrainLogic>();
        builder.Services.AddScoped<IAuthenticationLogic, AuthenticationLogic>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.EnsureIdentityDbIsCreated();
            app.SeedIdentityDataAsync().Wait();

        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

