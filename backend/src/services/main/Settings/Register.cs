using System;
using dotenv.net;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Services.Abstractions.Settings;

namespace PetStore.Services.Settings
{
    public static class Register
    {
        public static void AddAppSettings(this IServiceCollection services)
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RUNNING_ENVIRONMENT")))
            {
                DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { ".env" }));
            }

            services.AddSingleton<IAppConfiguration, AppConfiguration>();
            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
        }
    }
}
