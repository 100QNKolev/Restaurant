using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data
{
    public class Table
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public bool IsSmokingAllowed { get; set; }
    }
}
