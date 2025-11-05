using System.ComponentModel.DataAnnotations;

namespace Mercado_libre_frontend.Models
{
    public class Usuario
    {
        [Display(Name = "Id")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Rol{ get; set; }


    }
}
