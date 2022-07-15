using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{
    public class CartItems
    {
        public CartItems()
        {
        }
        
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double PriceUnit { get; set; }
        public string UnMed { get; set; }

    }
}