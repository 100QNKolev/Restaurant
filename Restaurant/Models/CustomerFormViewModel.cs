using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class CustomerFormViewModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
