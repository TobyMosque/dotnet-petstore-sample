using PetStore.Services.Abstractions.Settings;

namespace PetStore.Services.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        private readonly IAppConfiguration _config;

        public DatabaseSettings(IAppConfiguration config)
        {
            _config = config;
        }

        public string ConnectionString => _config.Root["PETSTORE_CONNECTION_STRING"];
    }
}
