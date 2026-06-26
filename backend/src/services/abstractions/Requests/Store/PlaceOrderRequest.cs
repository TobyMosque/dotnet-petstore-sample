using System;
using Mediator;
using PetStore.Services.Abstractions.Dtos;

namespace PetStore.Services.Abstractions.Requests.Store
{
    public class PlaceOrderRequest : IRequest<OrderDto>
    {
        public Guid? PetId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ShipDate { get; set; }
        public string Status { get; set; }
        public bool Complete { get; set; }
    }
}
