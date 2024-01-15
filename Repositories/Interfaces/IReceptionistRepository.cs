using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IReceptionistRepository
    {
        ICollection<Receptionist> GetReceptionists();
        Receptionist GetReceptionist(int id);
        Hotel GetReceptionistHotel(int id);
        bool CreateReceptionist(Receptionist receptionist);
        bool UpdateReceptionist(Receptionist receptionist);
        bool DeleteReceptionist(Receptionist receptionist);
        bool ReceptionistExists(int id);
        bool Save();
    }
}
