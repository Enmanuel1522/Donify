using System.ComponentModel.DataAnnotations;

namespace Donify.API.DTOs
{
    public class DonorDto
    {

        
        [Required(ErrorMessage = "El campo id esta vacio")]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastNeme { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
