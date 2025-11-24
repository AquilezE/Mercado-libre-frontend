using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mercado_libre_frontend.Models
{
    public class PedidoItem
    {
        [Display(Name = "Item ID")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Display(Name = "Pedido ID")]
        [JsonPropertyName("pedidoId")]
        public int PedidoId { get; set; } 

        [Display(Name = "Producto ID")]
        [JsonPropertyName("productoId")]
        public int ProductoId { get; set; }  

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [JsonPropertyName("precioUnitario")]
        public decimal PrecioUnitario { get; set; }
        public Producto? Producto { get; set; }
    }
}
