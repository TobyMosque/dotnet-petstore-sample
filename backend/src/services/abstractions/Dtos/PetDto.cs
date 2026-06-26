using System;
using System.Collections.Generic;

namespace PetStore.Services.Abstractions.Dtos
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public CategoryDto Category { get; set; }
        public IList<string> PhotoUrls { get; set; }
        public IList<TagDto> Tags { get; set; }
    }
}
