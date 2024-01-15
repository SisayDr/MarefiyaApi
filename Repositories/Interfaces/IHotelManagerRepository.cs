using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IHotelManagerRepository
    {
        ICollection<HotelManager> GetHotelManagers();
        HotelManager GetHotelManager(int id);
        HotelManager HotelManagerLogin(string Email, string Password);   
        Hotel GetHotel(int managerId);
        ICollection<Room> GetRooms(int managerId);
        HotelManager CreateHotelManager(HotelManager hotelManager);
        bool UpdateHotelManager(HotelManager hotelManager);
        bool DeleteHotelManager(HotelManager hotelManager);
        bool HotelManagerExists(int id);
        bool Save();
    }
}
