using System;

namespace PetStore.Domain.Entities
{
    public class PetPhoto
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }
        public string Url { get; set; }
        public Pet Pet { get; set; }
    }
}
