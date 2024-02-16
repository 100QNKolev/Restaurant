using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class ReservationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public bool IsSmokingAllowed { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Start { get; set; } = string.Empty;

        [Required]
        public string End { get; set; } = string.Empty;
    }
}
