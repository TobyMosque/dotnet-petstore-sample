using System;
using System.Collections.Generic;

namespace PetStore.Domain.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public string Status { get; set; }
        public Category Category { get; set; }
        public ICollection<PetPhoto> PhotoUrls { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
