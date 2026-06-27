using System.Collections.Generic;
using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Store
{
    public class GetAllOrdersRequest : IRequest<IList<OrderDto>>
    {
    }
}
