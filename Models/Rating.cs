using Microsoft.CodeAnalysis;

namespace MarefiyaApi.Models
{
    public class Rating
    {
        public decimal LocationRating { get; set; }
        public decimal HygieneRating { get; set; }
        public decimal CustomerServiceRating { get; set; }
        public decimal AverageRating { get; set; }

        public Rating(decimal Location, decimal Hygiene, decimal CustomerService)
        {
            this.LocationRating = Location;
            this.HygieneRating = Hygiene;
            this.CustomerServiceRating = CustomerService;
            this.AverageRating = (LocationRating+HygieneRating+CustomerServiceRating) / 3;
        }
    }
}
