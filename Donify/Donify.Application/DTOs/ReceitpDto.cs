using System.ComponentModel.DataAnnotations;

namespace Donify.Application.DTOs
{
    public class ReceiptDto
    {
        public int Id { get; set; }

        [Required]
        public int DonationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReceiptNumber { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        public bool SentByEmail { get; set; } = false;
    }
}