using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Helper
{
    public class SqlConnectionContext : ISqlConnectionContext
    {
        private readonly IConfiguration _configuration;
        private readonly Lazy<string> _connectionString;

        public SqlConnectionContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = new Lazy<string>(ResolveConnectionString);
        }

        public string GetConnectionString() => _connectionString.Value;

        private string ResolveConnectionString()
        {
            var configuredConnection = _configuration.GetConnectionString("StudentDb");
            if (!string.IsNullOrWhiteSpace(configuredConnection))
                return configuredConnection;

            var secretArn = _configuration["StudentDbSecretArn"];
            if (string.IsNullOrWhiteSpace(secretArn))
            {
                throw new InvalidOperationException(
                    "Database configuration is missing. Set ConnectionStrings__StudentDb for local development or StudentDbSecretArn for AWS Lambda.");
            }

            using var client = new AmazonSecretsManagerClient();
            var response = client.GetSecretValueAsync(new GetSecretValueRequest { SecretId = secretArn })
                .GetAwaiter().GetResult();
            return response.SecretString
                ?? throw new InvalidOperationException("The configured database secret does not contain a SecretString value.");
        }
    }
}
