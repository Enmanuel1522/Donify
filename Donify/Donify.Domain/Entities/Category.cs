using Donify.Domain.Core;

namespace Donify.Domain.Entities
{
    public class Category : HasId
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}