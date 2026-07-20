using Donify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Donify.Application.DTOs
{
    public class DonationDto
    {
        
        [Required(ErrorMessage = "El campo id esta vacio")]
        public int Id { get; set; }
        public int DonorId { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";

    }
}



