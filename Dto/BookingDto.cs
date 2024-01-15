namespace MarefiyaApi.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Double TotalPrice { get; set; }
        public int NoOfGuests { get; set; }
        public string Status { get; set; }
    }
}
