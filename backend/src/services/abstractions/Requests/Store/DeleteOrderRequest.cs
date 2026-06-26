using System;
using MediatR;

namespace PetStore.Services.Abstractions.Requests.Store
{
    public class DeleteOrderRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
