using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;

namespace MarefiyaApi.Repositories.Implementations
{
    public class HotelRepository : IHotelRepository
    {
        private readonly MarefiyaDbContext _context;

        public HotelRepository(MarefiyaDbContext context)
        {
            _context = context;
        }

        public ICollection<Hotel> GetHotels()
        {
            return _context.Hotels.ToList();
        }
        public ICollection<Hotel> GetHotelsByMinPrice(double MinPrice)
        {
            return _context.Hotels.Where(hotel => hotel.CheapestPrice > MinPrice).ToList();
        }
        public ICollection<Hotel> GetHotelsByMaxPrice(double MaxPrice)
        {
            return _context.Hotels.Where(hotel => hotel.CheapestPrice < MaxPrice).ToList();
        }
        public ICollection<Hotel> GetHotelsByMinMaxPrice(double MinPrice, double MaxPrice)
        {
            return _context.Hotels.Where(hotel => hotel.CheapestPrice > MinPrice && hotel.CheapestPrice <  MaxPrice).ToList();
        }
        public HotelManager GetHotelManager(int HotelId)
        {
            return _context.Hotels.Where(hotel => hotel.HotelId == HotelId).Select(hotel => hotel.HotelManager).FirstOrDefault();
        }
        public int GetHotelsCount()
        {
            return _context.Hotels.Count();
        }
        public Hotel GetHotel(int HotelId)
        {
            return _context.Hotels.Where(hotel => hotel.HotelId == HotelId).FirstOrDefault();
        }
        public ICollection<Booking> GetHotelBookings(int HotelId)
        {
            return _context.Bookings.Where(b => b.Hotel.HotelId == HotelId).ToList();
        }

        public int GetHotelBookingsCount(int HotelId)
        {
            return _context.Bookings.Where(b => b.Hotel.HotelId == HotelId).Count();
        }

        public Rating GetHotelRating(int HotelId)
        {
            var hotelReviews = _context.Reviews.Where(r => r.Hotel.HotelId == HotelId);
            if (hotelReviews.Count() <= 0)
                return new Rating(0, 0, 0);
            var ratings = new Rating(
                    (decimal)hotelReviews.Sum(review => review.LocationRating) / hotelReviews.Count(),
                    (decimal)hotelReviews.Sum(review => review.HygieneRating) / hotelReviews.Count(),
                    (decimal)hotelReviews.Sum(review => review.CustomerServiceRating) / hotelReviews.Count()
            );

            return ratings;
        }

        public ICollection<Review> GetHotelReviews(int HotelId)
        {
            return _context.Reviews.Where(r => r.Hotel.HotelId == HotelId).ToList();
        }

        public ICollection<Room> GetHotelRooms(int HotelId)
        {
            return _context.Rooms.Where(r => r.Hotel.HotelId == HotelId).ToList();
        }

        public int GetHotelRoomsCount(int HotelId)
        {
            return _context.Rooms.Where(r => r.Hotel.HotelId == HotelId).Count();
        }

        public ICollection<Hotel> GetHotelsByState(string state)
        {
            return _context.Hotels.Where(h => h.State == state).ToList();
        }
        public int GetHotelsCountByState(string state)
        {
            return _context.Hotels.Where(h => h.State == state).Count();
        }

        public ICollection<Hotel> GetHotelsByCity(string city)
        {
            return _context.Hotels.Where(h => h.City == city).ToList();
        }

        public int GetHotelsCountByCity(string city)
        {
            return _context.Hotels.Where(h => h.City == city).Count();
        }

        public bool CreateHotel(Hotel hotel)
        {
            _context.Add(hotel);
            return Save();
        }

        public bool UpdateHotel(Hotel hotel)
        {
            _context.Update(hotel);
            return Save();
        }

        public bool DeleteHotel(Hotel hotel)
        {
            _context.Remove(hotel);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool HotelExists(int HotelId)
        {
            return _context.Hotels.Any(hotel => hotel.HotelId == HotelId);
        }
        public bool HotelExists(string Name)
        {
            return _context.Hotels.Any(hotel => hotel.Name == Name);
        }

        public ICollection<Hotel> GetTopRatedHotels()
        {
            return _context.Hotels.Take(5).ToList();
        }

        public ICollection<Hotel> GetCheapestHotels()
        {
            return _context.Hotels.OrderBy(hotel => hotel.CheapestPrice).Take(5).ToList();
        }
    }
}