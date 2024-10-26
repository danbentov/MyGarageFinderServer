namespace MyGarageFinderServer.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
        public string UserPassword { get; set; } 
        public int UserStatusID { get; set; }
        public string Phone { get; set; }
        public int GarageLicense { get; set; }
    }
}
