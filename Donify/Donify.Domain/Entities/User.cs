using Donify.Domain.Core;

namespace Donify.API.Models.Entities
{
    public class User : HasId
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Operator,
        Auditor
    }
}