namespace MarefiyaApi.Dto
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string[]? Photos { get; set; }
        public double CheapestPrice { get; set; }
    }
}
