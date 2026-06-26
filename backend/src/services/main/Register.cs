using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Domain;
using PetStore.Services.Abstractions.Services;
using PetStore.Services.Settings;
using RT.Comb;

[assembly: MediatorOptions(ServiceLifetime = ServiceLifetime.Transient)]

namespace PetStore.Services
{
    public static class Register
    {
        public static void AddPetStoreServices(this IServiceCollection services)
        {
            services.AddAppSettings();

            services.AddSingleton<ICombProvider>(new PostgreSqlCombProvider(new UnixDateTimeStrategy()));

            services.AddDbContext<PetStoreContext>((sp, opts) =>
            {
                var db = sp.GetRequiredService<PetStore.Services.Abstractions.Settings.IDatabaseSettings>();
                opts.UseNpgsql(db.ConnectionString);
            });

            services.AddMediator();

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();
        }
    }
}
