using Eatagram.Core.MongoDb.Configuration;

namespace Eatagram.Core.Api.Config
{
    public static class DatabaseProviderExtensions
    {

        public static void ConfigureDatabases(this WebApplicationBuilder builder)
        {
            if (!builder.Environment.IsDevelopment())
            {
                builder.Services.SetupIdentityDatabase(builder.Configuration, AzureKeyVaultConfig.GetSqlConnectionString());
                builder.Services.Configure<MessagesStoreDatabaseSettings>(
                    config =>
                    {
                        config.ConnectionString = AzureKeyVaultConfig.GetMongoConnectionString();
                        config.DatabaseName = builder.Configuration["MessageStoreDatabase:DatabaseName"];
                        config.MessagesCollectionName = builder.Configuration["MessageStoreDatabase:MessagesCollectionName"];
                    });
            }
            else 
            {
                builder.Services.SetupIdentityDatabase(builder.Configuration, 
                    builder.Configuration["ConnectionStrings:SqlLocal"]);

                builder.Services.Configure<MessagesStoreDatabaseSettings>(
                    config =>
                    {
                        config.ConnectionString = builder.Configuration["MessagesStoreDatabase:ConnectionString"];
                        config.DatabaseName = builder.Configuration["MessageStoreDatabase:DatabaseName"];
                        config.MessagesCollectionName = builder.Configuration["MessageStoreDatabase:MessagesCollectionName"];
                    });
            }


        }
    }
}
