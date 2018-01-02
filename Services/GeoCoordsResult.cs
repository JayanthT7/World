namespace WebApplication1.Services
{
    public class GeoCoordsResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }
    }
}