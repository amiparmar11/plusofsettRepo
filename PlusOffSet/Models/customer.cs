using System.ComponentModel.DataAnnotations;

namespace PlusOffSet.Models
{
    public class customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
