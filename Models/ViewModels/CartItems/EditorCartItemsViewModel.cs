using System.ComponentModel.DataAnnotations;

namespace Mercadinho.Models.ViewModels.CartItems
{
    public class EditorCartItemsViewModel  
    {
        [Required(ErrorMessage = "O Item é obrigatório")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "A Quantidade é obrigatório")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "O Preço Unitário é obrigatório")]
        public double PriceUnit { get; set; }

        [Required(ErrorMessage = "A Unidade de Medida é obrigatório")]
        public string UnMed { get; set; }
    }
}