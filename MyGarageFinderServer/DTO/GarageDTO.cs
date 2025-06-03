namespace MyGarageFinderServer.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int? GarageNumber { get; set; }
        public string GarageName { get; set; }
        public string TypeCode { get; set; }
        public string GarageType { get; set; }
        public string GarageAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; } // Changed to string
        public int? ZipCode { get; set; }
        public int? SpecializationCode { get; set; }
        public string Specialization { get; set; }
        public string GarageManager { get; set; }
        public string GarageLicense { get; set; }
        public DateOnly? TestTime { get; set; }
        public string WorkingHours { get; set; }
   
        public List<int?> GarageSpecs { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double distance { get; set; }


        public GarageDTO() { }
        public GarageDTO(Models.Garage modelGarage)
        {
            this.GarageID = modelGarage.GarageId;
            this.GarageNumber = modelGarage.GarageNumber;
            this.GarageName = modelGarage.GarageName;
            this.TypeCode = modelGarage.TypeCode;
            this.GarageType = modelGarage.GarageType;
            this.GarageAddress = modelGarage.GarageAddress;
            this.City = modelGarage.City;
            this.Phone = modelGarage.Phone;
            this.ZipCode = modelGarage.ZipCode;
            this.SpecializationCode = modelGarage.SpecializationCode;
            this.Specialization = modelGarage.Specialization;
            this.GarageManager = modelGarage.GarageManager;
            this.GarageLicense = modelGarage.GarageLicense;
            this.TestTime = modelGarage.TestTime;
            this.WorkingHours = modelGarage.WorkingHours;
            this.Latitude = 0;
            this.Longitude = 0;
            this.distance = 0;
        }

        public Models.Garage GetModels()
        {
            Models.Garage modelsGarage = new Models.Garage()
            {
                GarageId = this.GarageID,
                GarageNumber = this.GarageNumber,
                GarageName = this.GarageName,
                TypeCode = this.TypeCode,
                GarageType = this.GarageType,
                GarageAddress = this.GarageAddress,
                City = this.City,
                Phone = this.Phone,
                ZipCode = this.ZipCode,
                SpecializationCode = this.SpecializationCode,
                Specialization = this.Specialization,
                GarageManager = this.GarageManager,
                GarageLicense = this.GarageLicense,
                TestTime = this.TestTime,
                WorkingHours = this.WorkingHours
            };

            return modelsGarage;
        }
    }
}
