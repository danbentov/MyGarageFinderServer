namespace MyGarageFinderServer.DTO
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string ReviewDescription { get; set; }
        public int UserID { get; set; }
        public int GarageID { get; set; }
        public DateTime ReviewTimestamp { get; set; }
    }
}
