namespace MyGarageFinderServer.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public int? GarageID { get; set; }
        public int? VehicleUserID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? StatusID { get; set; }
        public string? Description { get; set; }



        public AppointmentDTO(Models.Appointment modelStudent)
        {
            this.AppointmentID = modelStudent.AppointmentId;
            this.GarageID = modelStudent.GarageId;
            this.VehicleUserID = modelStudent.VehicleUserId;
            this.AppointmentDate = modelStudent.AppointmentDate;
            this.StatusID = modelStudent.StatusId;
            this.Description = modelStudent.AppoitmentDescription;


        }


        public Models.Appointment GetModels()
        {
            Models.Appointment modelsUser = new Models.Appointment()
            {
                AppointmentId = this.AppointmentID,
                AppointmentDate = this.AppointmentDate,
                GarageId = this.GarageID,
                VehicleUserId = this.VehicleUserID,
                StatusId = this.StatusID,
                AppoitmentDescription = this.Description,
            };
            return modelsUser;
        }


    }
}

