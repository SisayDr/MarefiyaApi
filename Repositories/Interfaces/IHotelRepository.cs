using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IHotelRepository
    {
        ICollection<Hotel> GetHotels();
        ICollection<Hotel> GetTopRatedHotels();
        ICollection<Hotel> GetCheapestHotels();
        Hotel GetHotel(int HotelId);
        ICollection<Hotel> GetHotelsByMinPrice(double MinPrice);
        ICollection<Hotel> GetHotelsByMaxPrice(double MaxPrice);
        ICollection<Hotel> GetHotelsByMinMaxPrice(double MinPrice, double MaxPrice);
        HotelManager GetHotelManager(int HotelId);
        int GetHotelsCount();
        ICollection<Hotel> GetHotelsByState(string state);
        int GetHotelsCountByState(string state);
        ICollection<Hotel> GetHotelsByCity(string city);
        int GetHotelsCountByCity(string state);
        ICollection<Room> GetHotelRooms(int HotelId);
        int GetHotelRoomsCount(int HotelId);
        ICollection<Booking> GetHotelBookings(int HotelId);
        ICollection<Review> GetHotelReviews(int HotelId);
        int GetHotelBookingsCount(int HotelId);
        Rating GetHotelRating(int HotelId);

        bool CreateHotel(Hotel hotel);
        bool UpdateHotel(Hotel hotel);
        bool DeleteHotel(Hotel hotel);
        bool Save();

        bool HotelExists(string Name);
        bool HotelExists(int HotelId);

    }
}
