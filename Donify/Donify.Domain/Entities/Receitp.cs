using Donify.Domain.Core;

namespace Donify.API.Models.Entities
{
    public class Receipt : HasId
    {
        public int DonationId { get; set; }
        public Donation? Donation { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public bool SentByEmail { get; set; } = false;
    }
}
