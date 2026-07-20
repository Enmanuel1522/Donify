using Donify.Domain.Core;

namespace Donify.Domain.Entities
{
    public class Staff : HasId
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime HiredAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}