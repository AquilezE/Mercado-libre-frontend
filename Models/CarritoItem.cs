using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Mercado_libre_frontend.Models
{
    public class CarritoItem
    {
        [Display(Name = "Item ID")]
        [JsonPropertyName("id")]
        public int ItemId { get; set; } 

        [Display(Name = "Producto ID")]
        public int ProductoId { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        [JsonPropertyName("precioUnitario")]
        public string PrecioUnitarioString { get; set; } = "0";

        [JsonIgnore]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal PrecioUnitario
        {
            get => decimal.TryParse(PrecioUnitarioString, out var result) ? result : 0m;
        }

        public Producto? Producto { get; set; }

    }
}
