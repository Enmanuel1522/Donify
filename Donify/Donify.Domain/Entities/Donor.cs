using Donify.Domain.Core;
using System.ComponentModel.Design;
using System.Data;

namespace Donify.API.Models.Entities
{
    public class Donor : HasId
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string DonorType { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();

    }

}


