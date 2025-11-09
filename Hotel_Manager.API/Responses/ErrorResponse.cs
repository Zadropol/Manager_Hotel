namespace Hotel_Manager.API.Responses
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
