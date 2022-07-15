using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{

    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}