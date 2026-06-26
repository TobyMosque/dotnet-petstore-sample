using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Api;
using PetStore.Domain;
using PetStore.Services;
using RT.Comb;
using Testcontainers.PostgreSql;
using Xunit;

namespace PetStore.Tests
{
    public class PetStoreFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _pg;
        public HttpClient Client { get; private set; }
        private WebApplicationFactory<Program> _factory;

        public PetStoreFixture()
        {
            var ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _pg = new PostgreSqlBuilder()
                .WithName($"test-db-{ts}")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _pg.StartAsync();

            var connStr = _pg.GetConnectionString();

            Environment.SetEnvironmentVariable("PETSTORE_CONNECTION_STRING", connStr);
            Environment.SetEnvironmentVariable("RUNNING_ENVIRONMENT", "test");

            _factory = new WebApplicationFactory<Program>();

            using (var scope = _factory.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<PetStoreContext>();
                await ctx.Database.EnsureCreatedAsync();

                var seedPath = Path.Combine(AppContext.BaseDirectory, "fixtures", "seed-data.7z");
                if (File.Exists(seedPath))
                {
                    var comb = scope.ServiceProvider.GetRequiredService<ICombProvider>();
                    var dataService = new DataService(ctx, comb);
                    await using var stream = File.OpenRead(seedPath);
                    await dataService.ImportAsync(stream, CancellationToken.None);
                }
            }

            Client = _factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            Client?.Dispose();
            if (_factory != null) await _factory.DisposeAsync();
            await _pg.DisposeAsync();
        }
    }
}
