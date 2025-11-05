using System.ComponentModel.DataAnnotations;
namespace Mercado_libre_frontend.Models
{
    public class Archivo
    {
        [Display(Name = "Id")]
        public int? ArchivoId { get; set; }

        [Display(Name = "MIME")]
        public string? Mime { get; set; }

        public string? Nombre { get; set; }

        [Display(Name = "Tamprecio")]
        public int? Size { get; set; }

        [Display(Name = "Repositorio")]
        public bool InDB { get; set; } = true;

    }
}
