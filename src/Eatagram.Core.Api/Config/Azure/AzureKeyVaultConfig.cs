using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace Eatagram.Core.Api.Config.Azure;

public static class AzureKeyVaultConfig
{
    private static readonly SecretClient _secretClient;
    private static readonly IDictionary<string, string> _connStrings = new Dictionary<string, string>();
    private static readonly SecretClientOptions _options = new()
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

        _secretClient = new SecretClient(new Uri("https://eatagramapikeyvault.vault.azure.net/"), new DefaultAzureCredential(), _options);

    }

    public static string GetMongoConnectionString()
    {
        if (!_connStrings.ContainsKey("Mongo"))
        {
            KeyVaultSecret connectionString = _secretClient.GetSecret("Eatagram-Api-MongoDB-Connection");

            _connStrings["Mongo"] = connectionString.Value;
        }

        return _connStrings["Mongo"];
    }

    public static string GetSqlConnectionString()
    {
        if (!_connStrings.ContainsKey("Sql"))
        {
            KeyVaultSecret connectionString =
                _secretClient.GetSecret("Eatagram-Api-Sql-Connection");

            _connStrings["Sql"] = connectionString.Value;
        }

        return _connStrings["Sql"];
    }
}