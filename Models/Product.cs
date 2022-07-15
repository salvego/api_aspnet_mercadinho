namespace Mercadinho.Models
{
    public class Product {

        public Product()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        //   ItemModel.fromJson(Map<String, dynamic> json)
        //       : id = json['id'],
        //         title = json['title'],
        //         picture = json['picture'],
        //         unit = json['unit'],
        //         price = json['price'].toDouble(),
        //         description = json['description'];

        //   Map<String, dynamic> toJson() {
        //     return {
        //       'id': id,
        //       'title': title,
        //       'picture': picture,
        //       'unit': unit,
        //       'price': price,
        //       'description': description,
        //     };
        //   }

        }

}