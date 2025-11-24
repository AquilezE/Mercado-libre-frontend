using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mercado_libre_frontend.Models
{
    public class Pedido
    {
        [Display(Name = "ID")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [JsonPropertyName("usuarioId")]
        public string UsuarioId { get; set; } = string.Empty;

        [Display(Name = "Dirección")]
        [JsonPropertyName("direccionId")]
        public int DireccionId { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [JsonPropertyName("total")]
        public decimal Total { get; set; }  

        [Display(Name = "Estado")]
        [JsonPropertyName("estado")]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "Fecha de Creación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [JsonPropertyName("creadoEn")]
        public DateTime? CreadoEn { get; set; }

        [Display(Name = "Fecha de Actualización")]
        [DataType(DataType.DateTime)]
        [JsonPropertyName("actualizadoEn")]
        public DateTime? ActualizadoEn { get; set; }

        [JsonPropertyName("items")]
        public List<PedidoItem> Items { get; set; } = [];

        public Direccion? Direccion { get; set; }

        // Alias for display compatibility
        public DateTime? FechaCreacion => CreadoEn;
    }
}
