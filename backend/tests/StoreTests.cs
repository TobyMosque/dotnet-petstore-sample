using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using Xunit;

namespace PetStore.Tests
{
    [Collection("PetStore")]
    public class StoreTests
    {
        private readonly PetStoreFixture _fixture;

        public StoreTests(PetStoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetInventory_ReturnsStatusCounts()
        {
            var response = await _fixture.Client.GetAsync("/store/inventory");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var inventory = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
            Assert.NotNull(inventory);
        }
    }
}
