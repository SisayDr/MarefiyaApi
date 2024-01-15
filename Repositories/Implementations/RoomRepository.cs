using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarefiyaApi.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MarefiyaDbContext _context;

        public RoomRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public Room GetRoom(int RoomId)
        {
            return _context.Rooms.Include(r => r.Hotel).Where(r => r.RoomId ==  RoomId).FirstOrDefault();
        }

        public ICollection<Booking> GetRoomBookings(int RoomId)
        {
            return _context.Bookings.Where(b => b.Room.RoomId == RoomId).ToList();
        }

        public ICollection<Review> GetRoomReviews(int RoomId)
        {
            return _context.Reviews.Where(r => r.Room.RoomId == RoomId).ToList();
        }

        public Rating GetRoomRating(int RoomId)
        {
            var roomReviews = _context.Reviews.Where(r => r.Hotel.HotelId == RoomId);
            if (roomReviews.Count() <= 0)
                return new Rating(0, 0, 0);
            var ratings = new Rating(
                    (decimal)roomReviews.Sum(review => review.LocationRating) / roomReviews.Count(),
                    (decimal)roomReviews.Sum(review => review.HygieneRating) / roomReviews.Count(),
                    (decimal)roomReviews.Sum(review => review.CustomerServiceRating) / roomReviews.Count()
            );

            return ratings;
        }

        public bool RoomExists(int RoomId)
        {
            return _context.Rooms.Any(r => r.RoomId == RoomId);
        }
        public bool HotelRoomExists(int HotelId, int RoomNo)
        {
            return _context.Rooms.Any(r => (r.Hotel.HotelId == HotelId) && (r.RoomNos.Contains(RoomNo)));
        }

        public ICollection<Room> GetRoomsByType(string RoomType)
        {
            return _context.Rooms.Where(r => r.RoomType == RoomType).ToList();
        }

        public int GetRoomsCountByType(string RoomType)
        {
            return _context.Rooms.Where(r => r.RoomType == RoomType).Count(); ;
        }

        public bool CreateRoom(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public bool UpdateRoom(Room room)
        {
            _context.Update(room);
            return Save(); 
        }

        public bool DeleteRoom(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
