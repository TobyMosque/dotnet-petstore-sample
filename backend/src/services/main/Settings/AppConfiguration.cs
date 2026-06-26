using Microsoft.Extensions.Configuration;
using PetStore.Services.Abstractions.Settings;

namespace PetStore.Services.Settings
{
    public class AppConfiguration : IAppConfiguration
    {
        public IConfigurationRoot Root { get; }

        public AppConfiguration()
        {
            Root = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
