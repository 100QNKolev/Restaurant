using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class ReserveTableViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
