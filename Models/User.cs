using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{

    public class User
    {
        public User()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? CPF { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public string? Slug { get; set; }

        public IList<Post>? Posts { get; set; }
        public IList<Role>? Roles { get; set; }

    }
}