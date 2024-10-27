namespace MyGarageFinderServer.DTO
{
    public class VehicleUserDTO
    {
        public int VehicleUserID { get; set; }

        public UserDTO User { get; set; } // User information
        public VehicleDTO Vehicle { get; set; } // Vehicle information
    }
}
