using MarefiyaApi.Models;

namespace MarefiyaApi.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Room GetRoom(int RoomId);
        ICollection<Room> GetRoomsByType(string RoomType);
        int GetRoomsCountByType(string RoomType);
        ICollection<Booking> GetRoomBookings(int RoomId);
        ICollection<Review> GetRoomReviews(int RoomId);
        Rating GetRoomRating(int RoomId);
        bool RoomExists(int RoomId);
        bool HotelRoomExists(int HotelId, int RoomNo);


        bool CreateRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool Save();

    }
}
