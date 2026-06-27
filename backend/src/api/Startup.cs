using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetStore.Domain;
using PetStore.Services;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPetStoreServices();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetStore v1"));

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            SeedIfEmpty(app);
        }

        private static void SeedIfEmpty(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PetStoreContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();

            WaitForDatabase(db, logger);

            db.Database.EnsureCreated();

            if (db.Pets.Any())
                return;

            var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
            dataService.SeedAsync(CancellationToken.None).GetAwaiter().GetResult();
            logger.LogInformation("Seed data imported from embedded resource.");
        }

        private static void WaitForDatabase(PetStoreContext db, ILogger<Startup> logger)
        {
            var retries = 10;
            while (retries-- > 0)
            {
                if (db.Database.CanConnect())
                    return;

                logger.LogWarning("Database not ready, retrying in 3 s... ({Retries} left)", retries);
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }

            throw new InvalidOperationException("Database did not become available after retries.");
        }
    }
}
