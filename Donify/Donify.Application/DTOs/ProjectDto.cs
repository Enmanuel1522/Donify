using System.ComponentModel.DataAnnotations;

namespace Donify.Application.DTOs
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
        public decimal RequiredBudget { get; set; }

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