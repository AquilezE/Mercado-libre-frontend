using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mercado_libre_frontend.Models
{
    public class Carrito
    {
        public List<CarritoItem> Items { get; set; } = [];

        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Total
        {
            get
            {
                return Items.Sum(item => item.PrecioUnitario * item.Cantidad);
            }
        }

        public int TotalItems
        {
            get
            {
                return Items.Sum(item => item.Cantidad);
            }
        }   
    }
}
