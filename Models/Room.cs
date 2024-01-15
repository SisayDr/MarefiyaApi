using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "Rooms", Schema = "Marefiya")]
    public class Room
    {
        public int RoomId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public int[] RoomNos { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Features { get; set; }
        [Column(TypeName = "money")]
        public double PricePerNight { get; set; }

        public Hotel Hotel { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
