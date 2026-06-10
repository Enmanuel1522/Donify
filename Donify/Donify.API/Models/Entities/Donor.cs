using System.ComponentModel.Design;
using System.Data;

namespace Donify.API.Models.Entities
{
    public class Donor
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastNeme { get; set; } 
        public string DonorType { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
