namespace Donify.API.Models.Entities
{
    public class Donation
    {
        public int Id { get; set; }

        public int DonorId { get; set; }
        public Donor? Donor { get; set; }

        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string? ReceiptUrl { get; set; }

    }
}



