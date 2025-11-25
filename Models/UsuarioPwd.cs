using System.ComponentModel.DataAnnotations;
namespace Mercado_libre_frontend.Models
{
    public class UsuarioPwd
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        [Display(Name = "Correo electrónico")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Rol { get; set; } = "Usuario";



    }
}
