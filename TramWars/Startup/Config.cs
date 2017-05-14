using Microsoft.Extensions.Configuration;

namespace TramWars.Startup
{
    public static class Config
    {
        private static IConfigurationRoot _configuration;

        public static void Init(bool isDevelopment)
        {
            var builder = new ConfigurationBuilder();
            if (isDevelopment)
            {
                builder.AddUserSecrets<Startup>();
            }

            _configuration = builder.Build();
        }

        public static string ConnectionString => _configuration[nameof(ConnectionString)];
        public static string TestConnectionString => _configuration[nameof(TestConnectionString)];
        public static string JwtAuthority => _configuration[nameof(JwtAuthority)];
    }   
}
