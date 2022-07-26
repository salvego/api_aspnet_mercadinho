namespace Mercadinho.Models.ViewModels.Orders
{
    public class ListOrdersViewModel
    {
        public int OrderId {get; set;}
        public DateTime CreatedDateTime {get; set;}
        public string StatusOrder {get; set;}
        public double Total {get; set;}
    }
}