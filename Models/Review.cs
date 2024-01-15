using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "Reviews", Schema = "Marefiya")]
    public class Review
    {
        public int Id { get; set; }
        public decimal LocationRating { get; set; }
        public decimal HygieneRating { get; set; }
        public decimal CustomerServiceRating { get; set; }
        public string Feedback { get; set; }

        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
