using System;

namespace PetStore.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid? PetId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ShipDate { get; set; }
        public string Status { get; set; }
        public bool Complete { get; set; }
    }
}
