using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Domain;
using PetStore.Services.Abstractions.Services;
using PetStore.Services.Settings;
using RT.Comb;

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

            services.AddMediatR(typeof(Register).Assembly);

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();
        }
    }
}
