namespace MarefiyaApi.Models
{
    public class StatusResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public StatusResponse(int statusCode, string message)
        {
            this.Status = statusCode;
            this.Message = message;
        }
    }
}
