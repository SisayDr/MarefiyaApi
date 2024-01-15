using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using SQLitePCL;

namespace MarefiyaApi.Repositories.Implementations
{
    public class HotelManagerRepository : IHotelManagerRepository
    {
        private readonly MarefiyaDbContext _context;

        public HotelManagerRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public HotelManager CreateHotelManager(HotelManager hotelManager)
        {
            _context.Add(hotelManager);
            return Save() ? hotelManager : null;
        }

        public bool DeleteHotelManager(HotelManager hotelManager)
        {
            _context.Remove(hotelManager);
            return Save();
        }
        public ICollection<HotelManager> GetHotelManagers()
        {
            return _context.HotelManagers.ToList();
        }

        public HotelManager GetHotelManager(int id)
        {
            return _context.HotelManagers.Where(manager => manager.Id == id).FirstOrDefault();  
        }

        public bool UpdateHotelManager(HotelManager hotelManager)
        {
            _context.Update(hotelManager);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool HotelManagerExists(int id)
        {
            return _context.HotelManagers.Any(hotelManager => hotelManager.Id == id);
        }

        public Hotel GetHotel(int managerId)
        {
            return _context.HotelManagers.Where(hotelManager => hotelManager.Id == managerId).Select(hotelManager => hotelManager.Hotel).FirstOrDefault();
        }

        public ICollection<Room> GetRooms(int managerId)
        {
            return _context.Rooms.Where(room => room.Hotel.HotelManagerId == managerId).ToList();
        }

        public HotelManager HotelManagerLogin(string Email, string Password)
        {
            return _context.HotelManagers.Where(hm => hm.Email == Email && hm.Password == Password).FirstOrDefault();
        }
    }
    
}
