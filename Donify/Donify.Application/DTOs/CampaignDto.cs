using System.ComponentModel.DataAnnotations;

namespace Donify.API.DTOs
{
    public class CampaignDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal GoalAmount { get; set; }

        public decimal CollectedAmount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Status { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
    }
}