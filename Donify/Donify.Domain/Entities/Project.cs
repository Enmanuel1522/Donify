using Donify.Domain.Core;

namespace Donify.Domain.Entities
{
    public class Project : HasId
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal RequiredBudget { get; set; }
        public decimal AssignedBudget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StaffId { get; set; }
    }
}