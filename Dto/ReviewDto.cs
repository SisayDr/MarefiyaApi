namespace MarefiyaApi.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public decimal LocationRating { get; set; }
        public decimal HygieneRating { get; set; }
        public decimal CustomerServiceRating { get; set; }
        public string Feedback { get; set; }
    }
}
