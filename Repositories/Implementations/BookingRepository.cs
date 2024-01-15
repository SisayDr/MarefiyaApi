using MarefiyaApi.Models;
using MarefiyaApi.Repositories.Interfaces;
using SQLitePCL;

namespace MarefiyaApi.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MarefiyaDbContext _context;

        public BookingRepository(MarefiyaDbContext context)
        {
            _context = context;
        }
        public bool CreateBooking(Booking booking)
        {
            _context.Add(booking);
            return Save();
        }

        public bool DeleteBooking(Booking booking)
        {
            _context.Remove(booking);
            return Save();
        }
        public ICollection<Booking> GetBookings()
        {
            return _context.Bookings.ToList();
        }

        public Booking GetBooking(int id)
        {
            return _context.Bookings.Where(booking => booking.Id == id).FirstOrDefault();
        }

        public bool UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool BookingExists(int id)
        {
            return _context.Bookings.Any(booking => booking.Id == id);
        }
    }
}