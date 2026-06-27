using System.Collections.Generic;
using Mediator;

namespace PetStore.Services.Abstractions.Requests.Store
{
    public class GetInventoryRequest : IRequest<IDictionary<string, int>>
    {
    }
}
