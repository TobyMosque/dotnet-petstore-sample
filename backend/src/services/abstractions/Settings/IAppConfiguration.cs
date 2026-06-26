using Microsoft.Extensions.Configuration;

namespace PetStore.Services.Abstractions.Settings
{
    public interface IAppConfiguration
    {
        IConfigurationRoot Root { get; }
    }
}
