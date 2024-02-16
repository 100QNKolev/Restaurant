using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class ReserveViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        public string Start { get; set; } = string.Empty;

        [Required]
        public string End { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public IList<ReserveCustomerViewModel> Customers { get; set; } = new List<ReserveCustomerViewModel>();
    }
}
