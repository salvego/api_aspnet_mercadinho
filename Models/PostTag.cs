using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{

    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}