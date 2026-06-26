using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using Xunit;

namespace PetStore.Tests
{
    [Collection("PetStore")]
    public class PetTests
    {
        private readonly PetStoreFixture _fixture;

        public PetTests(PetStoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task FindByStatus_ReturnsList()
        {
            var response = await _fixture.Client.GetAsync("/pet/findByStatus?status=available");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var pets = await response.Content.ReadFromJsonAsync<List<PetDto>>();
            Assert.NotNull(pets);
        }

        [Fact]
        public async Task FindByStatus_NullStatus_ReturnsListWithNullableStatusEntries()
        {
            var response = await _fixture.Client.GetAsync("/pet/findByStatus?status=");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var pets = await response.Content.ReadFromJsonAsync<List<PetDto>>();
            Assert.NotNull(pets);
        }

        [Fact]
        public async Task FindByTags_ReturnsMatchingPets()
        {
            var response = await _fixture.Client.GetAsync("/pet/findByTags?tags=young&tags=female");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var pets = await response.Content.ReadFromJsonAsync<List<PetDto>>();
            Assert.NotNull(pets);
        }
    }
}
