using System.ComponentModel.DataAnnotations;

namespace Phone.Book
{
    public class CreateContactDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
