namespace MyGarageFinderServer.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public int GarageID { get; set; }
        public int VehicleUserID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
    }
}
