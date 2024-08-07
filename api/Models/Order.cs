
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public float amount { get; set; }

// many to one with orderitems
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}  