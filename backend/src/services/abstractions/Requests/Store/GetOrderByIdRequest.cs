using System;
using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Store
{
    public class GetOrderByIdRequest : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
}
