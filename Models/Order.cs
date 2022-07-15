namespace Mercadinho.Models
{   
    public class Order 
    {
        public Order(){

        }
        public Order(int id, 
                     DateTime createdDateTime, 
                     DateTime overdueDateTime, 
                     string statusOrder, 
                     string copyAndPaste, 
                     double total)
        {
            Id = id;
            CreatedDateTime = createdDateTime;
            OverdueDateTime = overdueDateTime;
            StatusOrder = statusOrder;
            CopyAndPaste = copyAndPaste;
            Total = total;
        }

        public int Id {get; set;}
        public DateTime CreatedDateTime {get; set;}
        public DateTime? OverdueDateTime {get; set;}
        public string StatusOrder {get; set;}
        public string? CopyAndPaste {get; set;}
        public double Total {get; set;}
    }
}