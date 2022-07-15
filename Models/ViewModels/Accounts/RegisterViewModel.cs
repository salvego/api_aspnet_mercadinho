using System.ComponentModel.DataAnnotations;

namespace Mercadinho.Models.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo E-mail é inválido")]
        public string Email { get; set; }

    }
}