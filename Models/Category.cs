using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mercadinho.Models
{

    public class Category
    {
        public Category()
        {
        }
        public Category(int id, 
                        string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }
               

    }
}