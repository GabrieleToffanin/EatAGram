using Eatagram.Core.MongoDb.Configuration;

namespace Eatagram.Core.Api.Config
{
    public static class DatabaseProviderExtensions
    {

        public static void ConfigureDatabases(this WebApplicationBuilder builder)
        {
            string sqlConnection = !builder.Environment.IsDevelopment() 
                ? AzureKeyVaultConfig.GetSqlConnectionString() : builder.Configuration["ConnectionStrings:SqlLocal"];
            string mongoDbConnection = !builder.Environment.IsDevelopment() 
                ? AzureKeyVaultConfig.GetMongoConnectionString() : builder.Configuration["MessagesStoreDatabase:ConnectionString"];



            builder.Services.SetupIdentityDatabase(builder.Configuration, sqlConnection);
            builder.Services.Configure<MessagesStoreDatabaseSettings>(
                config =>
                {
                    config.ConnectionString = mongoDbConnection;
                    config.DatabaseName = builder.Configuration["MessageStoreDatabase:DatabaseName"];
                    config.MessagesCollectionName = builder.Configuration["MessageStoreDatabase:MessagesCollectionName"];
                    config.ChatUsersCollectionName = builder.Configuration["MessagesStoreDatabase:ChatUserCollectionName"];
                    config.ConnectionsCollectionName = builder.Configuration["MessagesStoreDatabase:ConnectionsCollectionName"];
                });
            


        }
    }
}
