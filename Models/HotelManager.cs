using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "HotelManagers", Schema = "Marefiya")]
    public class HotelManager: User
    {
        public Hotel Hotel { get; set; }
    }
}
