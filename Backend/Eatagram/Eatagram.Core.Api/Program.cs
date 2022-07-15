using Eatagram.Core.Api.Config;
using Eatagram.Core.Api.Hubs;
using Eatagram.Core.Data.EntityFramework.Repository;
using Eatagram.Core.Interfaces.Comments;
using Eatagram.Core.Interfaces.Logic;
using Eatagram.Core.Interfaces.Messaging;
using Eatagram.Core.Interfaces.Repository;
using Eatagram.Core.Logic;
using Eatagram.Core.MongoDb.DatabaseService;
using Eatagram.Core.MongoDb.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

public partial class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors();

        builder.Services.AddSwaggerGen(configuration => configuration.ConfigureSwagger());

        builder.ConfigureDatabases();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRecipeRepository, RecipesRepository>();
        builder.Services.AddScoped<IRecipeBrainLogic, RecipeBrainLogic>();
        builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
        builder.Services.AddScoped<ICommentsLogic, CommentsLogic>();
        builder.Services.AddScoped<IUserMessaging, UserMessagingLogic>();
        builder.Services.AddScoped<IUserMessagingRepository, UserMessagingRepository>();
        builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
        builder.Services.AddScoped<IMessagingLogic, MessagingLogic>();
        builder.Services.AddSingleton<MessagesDb>();


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(builder.Configuration)
                        .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddInMemoryTokenCaches();



        builder.Services.AddSignalR();


        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            
        }

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();
        app.UseRouting();


        app.UseCors(options => options
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetIsOriginAllowed(origin => true));

        app.MapControllers();




        app.MapControllers();



        app.Run();
    }
}


