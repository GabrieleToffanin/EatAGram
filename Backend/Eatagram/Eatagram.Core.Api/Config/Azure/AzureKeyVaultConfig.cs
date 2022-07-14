using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace Eatagram.Core.Api.Config
{
    public static class AzureKeyVaultConfig
    {
        private static readonly SecretClient _secretClient;
        private static readonly Dictionary<string, string> connStrings = new Dictionary<string, string>();
        private static readonly SecretClientOptions options = new SecretClientOptions
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };

        static AzureKeyVaultConfig()
        {

            _secretClient = new SecretClient(new Uri("https://eatagramapikeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);

        }

        public static string GetMongoConnectionString()
        {
            if (!connStrings.ContainsKey("Mongo"))
            {
                KeyVaultSecret connectionString =  _secretClient.GetSecret("Eatagram-Api-MongoDB-Connection");

                connStrings["Mongo"] = connectionString.Value;
            }

            return connStrings["Mongo"];
        }

        public static string GetSqlConnectionString()
        {
            if (!connStrings.ContainsKey("Sql"))
            {
                KeyVaultSecret connectionString = 
                    _secretClient.GetSecret("Eatagram-Api-Sql-Connection");

                connStrings["Sql"] = connectionString.Value;
            }

            return connStrings["Sql"];
        }
    }
}
