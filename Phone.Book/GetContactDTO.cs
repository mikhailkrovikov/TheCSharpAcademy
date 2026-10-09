using System.ComponentModel.DataAnnotations;

namespace Phone.Book
{
    public class GetContactDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
