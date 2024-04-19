
namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public float amount { get; set; }
        
// one to mant with users
        public int? UserId { get; set; }

        public User? User { get; set; }
//end 

// many to one with orderitems
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
} 