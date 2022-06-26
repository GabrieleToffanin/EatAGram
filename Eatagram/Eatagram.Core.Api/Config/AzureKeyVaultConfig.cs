using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Eatagram.Core.Api.Config
{
    public static class AzureKeyVaultConfig
    {
        public static void SetupAzureSecrets(out string sql, out string mongo)
        {
            var client = new SecretClient(new Uri("https://eatagramapikeyvault.vault.azure.net/"), new DefaultAzureCredential());

            KeyVaultSecret mongoSecret = client.GetSecret("Eatagram-Api-MongoDB-Connection");
            KeyVaultSecret sqlSecret = client.GetSecret("Eatagram-Api-Sql-Connection");

            mongo = mongoSecret.Value;
            sql = sqlSecret.Value;
        }
    }
}
