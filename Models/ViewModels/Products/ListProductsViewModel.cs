namespace Mercadinho.Models.ViewModels.Products
{
    public class ListProductsViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public string? Picture { get; set; }
    public string? Unit { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
}
}