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

            var item1 = new ContactEntity
            {
                Id = 1,
                Name = "Mikrozaimy",
                PhoneNumber = "+78005553535",
                Email = "blya_budu@mail.ru"
            };

            var danek = new ContactEntity
            {
                Id = 2,
                Name = "Danek",
                PhoneNumber = "+79992281488",
                Email = "karlik@mail.ru"
            };

            builder.HasData(item1, danek);
        }
    }
}
