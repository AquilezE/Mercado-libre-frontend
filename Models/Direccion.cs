using System.ComponentModel.DataAnnotations;
namespace Mercado_libre_frontend.Models
{
    public class Direccion
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La calle es obligatoria")]
        [StringLength(200, ErrorMessage = "La calle no puede exceder 200 caracteres")]
        [Display(Name = "Calle")]
        public string Calle { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(100, ErrorMessage = "La ciudad no puede exceder 100 caracteres")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe tener exactamente 5 dígitos")]
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; } = string.Empty;
    }
}
