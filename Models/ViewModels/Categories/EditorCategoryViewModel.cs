using System.ComponentModel.DataAnnotations;

namespace Mercadinho.Models.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O campo Title é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 a 40 caracteres")]
        public string Title { get; set; }
    }
}