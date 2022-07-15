namespace Mercadinho.Models.ViewModels.CartItems
{
    public class ListCartItemsViewModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double PriceUnit { get; set; }
        public string UnMed { get; set; }

    }
}