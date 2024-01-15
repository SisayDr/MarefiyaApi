using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "Bookings", Schema = "Marefiya")]
    public class Booking
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfGuests { get; set; }
        public Double TotalPrice { get; set; }
        public string Status { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public Customer Customer { get; set; } 
    }
}
