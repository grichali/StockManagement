
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Order
    {
        [Key] // Specify that this property is the primary key
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public float amount { get; set; }
        
// one to many with users
        public int UserId { get; set; }

        
        public User User { get; set; }
//end 

// many to one with orderitems
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}  