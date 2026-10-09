using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Phone.Book
{
    public class ContactConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasIndex(c => c.Email)
                .IsUnique();

            builder
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();
        }
    }
}
