using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name: "Receptionists", Schema = "Marefiya")]
    public class Receptionist: User
    {
        public Hotel Hotel { get; set; }
    }
}
