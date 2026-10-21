using Microsoft.EntityFrameworkCore;

namespace Phone.Book
{
    [EntityTypeConfiguration(typeof(ContactConfiguration))]
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Group { get; set; } = "";
    }
}
