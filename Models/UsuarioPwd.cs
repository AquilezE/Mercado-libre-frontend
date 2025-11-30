using System.ComponentModel.DataAnnotations;
namespace Mercado_libre_frontend.Models
{
    public class UsuarioPwd
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
	        [RegularExpression(@"^(?=.*[A-ZÑ])(?=.*[a-zñ])(?=.*\d)(?=.*[!#$%&/()=?])[A-Za-zÑñ\d!#$%&/()=?]{8,255}$",
				            ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluir mayúscula, minúscula, número y símbolo.")]
			        [DataType(DataType.Password)]
				        [Display(Name = "Contraseña")]
					        public required string Password { get; set; }

	        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
		        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", 
					            ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
				        [Display(Name = "Correo electrónico")]
					        public required string Email { get; set; }

		        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
			        public required string Nombre { get; set; }

			        public string Rol { get; set; } = "Usuario";


    }
}
