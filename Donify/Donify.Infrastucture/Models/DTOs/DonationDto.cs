using Donify.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Donify.API.DTOs
{
    public class DonationDto
    {
        //[Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor que 0")]
        [Required(ErrorMessage = "El campo id esta vacio")]
        public int Id { get; set; }
        public int DonorId { get; set; }
        public Donor? Donor { get; set; }

        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";

    }
}



