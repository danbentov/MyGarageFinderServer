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

        public VehicleDTO() { }
        public VehicleDTO(Models.Vehicle modelvehicle)
        {
            this.LicensePlate = modelvehicle.LicensePlate;
            this.Model = modelvehicle.Model;
            this.VehicleYear = modelvehicle.VehicleYear;
            this.FuelType = modelvehicle.FuelType;
            this.Color = modelvehicle.Color;
            this.Manufacturer = modelvehicle.Manufacturer;
            this.CurrentMileage = modelvehicle.CurrentMileage;
            this.ImageURL = modelvehicle.ImageUrl;
        }

        public Models.Vehicle GetVehicle()
        {
            Models.Vehicle vehicle = new Models.Vehicle
            {
                LicensePlate = this.LicensePlate,
                Model = this.Model,
                VehicleYear = this.VehicleYear,
                FuelType = this.FuelType,
                Color = this.Color,
                Manufacturer = this.Manufacturer,
                CurrentMileage = this.CurrentMileage,
                ImageUrl = this.ImageURL
            };
            return vehicle;
            
        } 
    }

}
