using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetStore.Services.Abstractions.Dtos;
using Xunit;

namespace PetStore.Tests
{
    [Collection("PetStore")]
    public class UserTests
    {
        private readonly PetStoreFixture _fixture;

        public UserTests(PetStoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetUserByName_Seeded_ReturnsUser()
        {
            var createResp = await _fixture.Client.PostAsJsonAsync("/user", new
            {
                Username = "testuser_fixture",
                FirstName = (string)null,
                LastName = (string)null,
                Email = (string)null,
                Password = "pw",
                Phone = (string)null,
                UserStatus = 0
            });
            Assert.Equal(HttpStatusCode.OK, createResp.StatusCode);

            var response = await _fixture.Client.GetAsync("/user/testuser_fixture");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            Assert.NotNull(user);
            Assert.Equal("testuser_fixture", user.Username);
        }
    }
}
