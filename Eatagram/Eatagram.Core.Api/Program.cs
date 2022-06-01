using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Data.EntityFramework.Repository;
using Eatagram.Core.Logic;
using Eatagram.Core.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:LocalSqlServer"],
    migrationsAsm => migrationsAsm.MigrationsAssembly("Eatagram.Core.Api")));


builder.Services.AddScoped<IRecipeRepository, RecipesRepository>();
builder.Services.AddScoped<IRecipeBrainLogic, RecipeBrainLogic>();



// Http header verb GET POST DELETE PATCH PUT body json 

//API
//BL business layer/ business logic/ core/
//DAL Data access layer

// lifetime request IRecipeBrainLogic            IRecipeBrainLogic  new RecipeBrainLogic
// httpRequest ->   middleware ->                bl -> dal -> bl -> controller -> httpResponse 

// Scoped Singleton Transient 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
        