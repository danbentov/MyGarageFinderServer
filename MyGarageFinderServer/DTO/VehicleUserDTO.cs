namespace MyGarageFinderServer.DTO
{
    public class VehicleUserDTO
    {
        public int VehicleUserID { get; set; }

        public UserDTO User { get; set; } // User information
        public VehicleDTO Vehicle { get; set; } // Vehicle information

        public VehicleUserDTO() { }
        public VehicleUserDTO(Models.VehicleUser modelVU)
        {
            this.VehicleUserID = modelVU.VehicleUserId;
            this.User = new UserDTO(modelVU.User);
            this.Vehicle = new VehicleDTO(modelVU.Vehicle);
        }

        public Models.VehicleUser GetModels()
        {
            Models.VehicleUser modelVU = new Models.VehicleUser()
            {
                VehicleUserId = this.VehicleUserID,
                User = this.User.GetModels(),
                Vehicle = this.Vehicle.GetVehicle()
            };

            return modelVU;
        }
    }
}
