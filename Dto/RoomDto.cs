namespace MarefiyaApi.Dto
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public int[] RoomNos { get; set; }
        public string Features { get; set; }
        public double PricePerNight { get; set; }
    }
}
