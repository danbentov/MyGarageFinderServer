namespace MyGarageFinderServer.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int GarageNumber { get; set; }
        public string GarageName { get; set; }
        public string TypeCode { get; set; }
        public string GarageType { get; set; }
        public string GarageAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; } // Changed to string
        public int ZipCode { get; set; }
        public int SpecializationCode { get; set; }
        public string Specialization { get; set; }
        public string GarageManager { get; set; }
        public int GarageLicense { get; set; }
        public DateTime TestTime { get; set; }
        public string WorkingHours { get; set; }
    }
}
