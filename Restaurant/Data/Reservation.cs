﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Data
{
    public class Reservation
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int TableId { get; set; }

        [ForeignKey(nameof(TableId))]
        public Table Table { get; set; } = null!;

        [Required]
        public DateTime ReservationTime { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}