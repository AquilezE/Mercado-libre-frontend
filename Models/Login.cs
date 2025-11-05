using System.ComponentModel.DataAnnotations;
namespace Mercado_libre_frontend.Models
{
    public class Login
    {
        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        [Display(Name = "Correo electrónico")]
        public required string Email { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }
    }
}
