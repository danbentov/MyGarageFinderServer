namespace MyGarageFinderServer.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string UserPassword { get; set; } = null!; 
        public int UserStatusId { get; set; }
        public string Phone { get; set; }
        public int GarageLicense { get; set; }

        public UserDTO() { }
        public UserDTO(Models.User modelUser)
        {
            this.UserId = modelUser.UserId;
            this.FirstName = modelUser.FirstName;
            this.LastName = modelUser.LastName;
            this.Email = modelUser.Email;
            this.LicenseNumber = modelUser.LicenseNumber;
            this.UserPassword = modelUser.UserPassword;
            this.UserStatusId = modelUser.UserStatusId;
            this.Phone = modelUser.Phone;
            this.GarageLicense = modelUser.GarageLicense;
        }

        public Models.User GetModels()
        {
            Models.User modelsUser = new Models.User()
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                LicenseNumber = this.LicenseNumber,
                UserPassword = this.UserPassword,
                UserStatusId = this.UserStatusId,
                Phone = this.Phone,
                GarageLicense = this.GarageLicense
            };

            return modelsUser;
        }
    }
}
