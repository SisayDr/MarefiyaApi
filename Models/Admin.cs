using System.ComponentModel.DataAnnotations.Schema;

namespace MarefiyaApi.Models
{
    [Table(name:"Admins", Schema = "Marefiya")]
    public class Admin: User
    {
    }
}
