using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }   
    }
}