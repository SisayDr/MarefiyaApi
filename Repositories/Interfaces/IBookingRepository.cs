using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        ICollection<Booking> GetBookings();
        Booking GetBooking(int id);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(Booking booking);
        bool BookingExists(int id);
        bool Save();
    }
}
