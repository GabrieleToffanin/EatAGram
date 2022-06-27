using Eatagram.Core.Api.Config;
using Eatagram.Core.Api.Extensions;
using Eatagram.Core.Api.Hubs;
using Eatagram.Core.Data.EntityFramework.Repository;
using Eatagram.Core.Entities.Token;
using Eatagram.Core.Logic;
using Eatagram.Core.MongoDb.Configuration;
using Eatagram.Core.MongoDb.DatabaseService;
using Eatagram.Core.MongoDb.Repository;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eatagram", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. <br>
                Enter 'Bearer' [space] and then your token in the text input below.
                <br>Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

        });

        builder.Services.Configure<JwtToken>(builder.Configuration.GetSection("token"));

        builder.Services.SetupIdentityDatabase(builder.Configuration);

        builder.Services.Configure<MessagesStoreDatabaseSettings>(
            builder.Configuration.GetSection("MessageStoreDatabase"));

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRecipeRepository, RecipesRepository>();
        builder.Services.AddScoped<IRecipeBrainLogic, RecipeBrainLogic>();
        builder.Services.AddScoped<IAuthenticationLogic, AuthenticationLogic>();
        builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
        builder.Services.AddScoped<IMessagingLogic, MessagingLogic>();
        builder.Services.AddSingleton<MessagesDb>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = builder.Configuration["token:issuer"],
                    ValidAudience = builder.Configuration["token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["token:secret"]))
                };
            });

        builder.Services.AddSignalR();


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
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
         
        app.UseCors(options =>
         options.WithOrigins("http://localhost:3000")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
       

        app.UseWebSockets();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MessagingHub>("/Chat");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=index}/{id?}");
        });

        app.Run();
    }
}


