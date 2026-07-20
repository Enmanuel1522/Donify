using Donify.Domain.Core;

namespace Donify.Domain.Entities
{
    public class Campaign : HasId
    {
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal GoalAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CampaignStatus Status { get; set; }

        public int CategoryId { get; set; }
    }

    public enum CampaignStatus
    {
        Active,
        Finished,
        Cancelled
    }
}
