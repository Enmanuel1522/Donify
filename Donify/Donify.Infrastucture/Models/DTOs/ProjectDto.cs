using System.ComponentModel.DataAnnotations;

namespace Donify.API.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "RequiredBudget must be greater than 0")]
        public decimal RequiredBudget { get; set; }

        [Range(0, double.MaxValue)]
        public decimal AssignedBudget { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int StaffId { get; set; }
    }
}