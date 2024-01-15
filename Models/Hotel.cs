using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "Hotels", Schema = "Marefiya")]
    public class Hotel
    {
        public int HotelId { get; set; }
        public int HotelManagerId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string State { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Place { get; set; }
        [Column(TypeName = "nvarchar(1024)")]
        public string Description { get; set; }
        public string[]? Photos { get; set; }
        public double CheapestPrice { get; set; }

        [ForeignKey("HotelManagerId")]
        public HotelManager HotelManager { get; set; }
        public ICollection<Receptionist> Receptionists { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
