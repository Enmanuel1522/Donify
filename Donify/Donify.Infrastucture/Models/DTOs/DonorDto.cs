using System.ComponentModel.DataAnnotations;

namespace Donify.API.DTOs
{
    public class DonorDto
    {

        //[Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor que 0")]
        [Required(ErrorMessage = "El campo id esta vacio")]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastNeme { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
