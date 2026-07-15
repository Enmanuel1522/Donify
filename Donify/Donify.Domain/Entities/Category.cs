using Donify.Domain.Core;

namespace Donify.API.Models.Entities
{
    public class Category : HasId
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}