using Xunit;

namespace PetStore.Tests
{
    [CollectionDefinition("PetStore")]
    public class PetStoreCollection : ICollectionFixture<PetStoreFixture> { }
}
