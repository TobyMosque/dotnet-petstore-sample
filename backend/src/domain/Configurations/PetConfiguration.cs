using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Domain.Entities;

namespace PetStore.Domain.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired();

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Pets)
                   .HasForeignKey(p => p.CategoryId);

            builder.HasMany(p => p.PhotoUrls)
                   .WithOne(ph => ph.Pet)
                   .HasForeignKey(ph => ph.PetId)
                   .IsRequired();

            builder.HasMany(p => p.Tags)
                   .WithMany(t => t.Pets);
        }
    }
}
