namespace MarefiyaApi.Models
{
    public class HotelDbView
    {
        public int HotelId { get; set; }
        public int HotelManagerId { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }
        public string[]? Photos { get; set; }
        public double CheapestPrice { get; set; }

        public decimal LocationRating { get; set; }
        public decimal HygieneRating { get; set; }
        public decimal CustomerServiceRating { get; set; }
        public decimal AverageRating { get; set; }
    }
}
