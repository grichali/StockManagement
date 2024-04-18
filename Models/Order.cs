
namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public float amount { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}