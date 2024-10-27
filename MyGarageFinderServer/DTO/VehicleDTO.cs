namespace MyGarageFinderServer.DTO
{
    public class VehicleDTO
    {
        public string LicensePlate { get; set; } // Changed to string if it contains letters
        public string Model { get; set; }
        public int VehicleYear { get; set; }
        public string FuelType { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public int CurrentMileage { get; set; }
        public string ImageURL { get; set; }
    }
}
