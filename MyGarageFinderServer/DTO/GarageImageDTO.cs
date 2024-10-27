namespace MyGarageFinderServer.DTO
{
    public class GarageImageDTO
    {
        public int GarageImageID { get; set; }
        public int GarageID { get; set; } // Refers to the Garage entity
        public string ImageURL { get; set; }
    }
}
