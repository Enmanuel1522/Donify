using System.ComponentModel.DataAnnotations;

namespace Donify.Application.DTOs
{
    public class DonorDto
    {

        
        [Required(ErrorMessage = "El campo id esta vacio")]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
