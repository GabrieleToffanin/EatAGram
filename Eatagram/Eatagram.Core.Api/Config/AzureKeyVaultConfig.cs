using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Eatagram.Core.Entities.Azure;
using Eatagram.Core.Interfaces.Azure;
using Microsoft.Extensions.Options;

namespace Eatagram.Core.Api.Config
{
    public sealed class AzureKeyVaultConfig : IConnectionStringsProvider
    {
        private readonly SecretClient _secretClient;
        private readonly AzureKeys _azureKeys;
        private readonly Dictionary<string, string> connStrings = new Dictionary<string, string>();

        public AzureKeyVaultConfig(IOptions<AzureKeys> keys)
        {
            _azureKeys = keys.Value;

            _secretClient = new SecretClient(new Uri(_azureKeys.Url), new DefaultAzureCredential());

        }

        public async ValueTask<string> GetMongoConnectionString()
        {
            if (!connStrings.ContainsKey("Mongo"))
            {
                KeyVaultSecret connectionString = await _secretClient.GetSecretAsync("Eatagram-Api-MongoDB-Connection");

                connStrings["Mongo"] = connectionString.Value;
            }

            return connStrings["Mongo"];
        }

        public async ValueTask<string> GetSqlConnectionString()
        {
            if (!connStrings.ContainsKey("Sql"))
            {
                KeyVaultSecret connectionString = await
                    _secretClient.GetSecretAsync("Eatagram-Api-Sql-Connection");

                connStrings["Sql"] = connectionString.Value;
            }

            return connStrings["Sql"];
        }
    }
}
