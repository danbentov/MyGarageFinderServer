using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using MyGarageFinderServer.DTO;
using MyGarageFinderServer.GarageAPIReaderService;
using MyGarageFinderServer.Models;
using System.Collections.ObjectModel;

[Route("api")]
[ApiController]
public class MyGarageFinderAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private MyGarageFinderDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    private readonly GarageService _garageService;

    public MyGarageFinderAPIController(MyGarageFinderDbContext context, IWebHostEnvironment env, GarageService garageService)
    {
        this.context = context;
        this.webHostEnvironment = env;
        _garageService = garageService;
    }

    [HttpGet]
    [Route("TestServer")]
    public ActionResult<string> TestServer()
    {
        return Ok("Server Responded Successfully");
    }



    [HttpPost("login")]
    public IActionResult Login([FromBody] MyGarageFinderServer.DTO.LoginInfo loginDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Get model user class from DB with matching email. 
            MyGarageFinderServer.Models.User? modelsUser = context.GetUser(loginDto.LicenseNumber);

            //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
            if (modelsUser == null || modelsUser.UserPassword != loginDto.UserPassword)
            {
                return Unauthorized();
            }

            //Login suceed! now mark login in session memory!
            HttpContext.Session.SetString("loggedInUser", modelsUser.LicenseNumber);

            MyGarageFinderServer.DTO.UserDTO dtoUser = new MyGarageFinderServer.DTO.UserDTO(modelsUser);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost("register")]
    public IActionResult Register([FromBody] MyGarageFinderServer.DTO.UserDTO userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            MyGarageFinderServer.Models.User modelsUser = userDto.GetModels();
            foreach (User u in context.Users)
            {
                if (u.LicenseNumber == modelsUser.LicenseNumber)
                    return Unauthorized();
            }
            context.Users.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            MyGarageFinderServer.DTO.UserDTO dtoUser = new MyGarageFinderServer.DTO.UserDTO(modelsUser);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("searchvehicle")]
    public IActionResult SearchVehicle([FromBody] MyGarageFinderServer.DTO.VehicleLicense licenseDto)
    {
        try
        {
            string lPlate = licenseDto.LicensePlate;
            foreach (Vehicle v in context.Vehicles)
            {
                if (v.LicensePlate == lPlate)
                {
                    MyGarageFinderServer.DTO.VehicleDTO vehicleDto = new MyGarageFinderServer.DTO.VehicleDTO(v);
                    return Ok(vehicleDto);
                }
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("registervehicle")]
    public IActionResult RegisterVehicle([FromBody] MyGarageFinderServer.DTO.VehicleUserDTO vhDTO)
    {
        try
        {

            MyGarageFinderServer.Models.Vehicle modelVehicle = vhDTO.Vehicle.GetVehicle();
            MyGarageFinderServer.Models.User? modelsUser = context.GetUser(vhDTO.User.LicenseNumber);

            bool vehicleExist = false;
            foreach (Vehicle v in context.Vehicles)
            {
                if (v.LicensePlate == modelVehicle.LicensePlate)
                {
                    vehicleExist = true;
                }
            }
            if (vehicleExist)
            {
                foreach (VehicleUser vu in context.VehicleUsers)
                {
                    if (vu.UserId == modelsUser.UserId && vu.VehicleId == modelVehicle.LicensePlate)
                        return BadRequest("You already registered this vehicle !!");
                }
            }

            if (!vehicleExist)
            {
                context.Vehicles.Add(modelVehicle);
            }
            context.SaveChanges();

            MyGarageFinderServer.Models.VehicleUser VU = new VehicleUser();
            VU.VehicleId = modelVehicle.LicensePlate;
            VU.UserId = modelsUser.UserId;
            context.VehicleUsers.Add(VU);
            context.SaveChanges();
            MyGarageFinderServer.DTO.VehicleUserDTO vuDto = new VehicleUserDTO(VU);
            return Ok(vuDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("myvehicles")]
    public IActionResult GetYourVehicles([FromBody] MyGarageFinderServer.DTO.UserDTO userDto)
    {
        try
        {
            MyGarageFinderServer.Models.User modelsUser = context.GetUser(userDto.LicenseNumber);
            ObservableCollection<Vehicle> userVehicles = context.GetVehicles(modelsUser);
            ObservableCollection<VehicleDTO> userVehiclesDto = new ObservableCollection<VehicleDTO>();
            foreach (Vehicle v in userVehicles)
            {
                VehicleDTO vDto = new VehicleDTO(v);
                userVehiclesDto.Add(vDto);
            }
            return Ok(userVehiclesDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("updateuser")]
    public IActionResult UpdateUser([FromBody] MyGarageFinderServer.DTO.UserDTO userDto)
    {
        try
        {
            MyGarageFinderServer.Models.User modelsUser = userDto.GetModels();
            User? currentUserFromDB = context.Users.Where(u => u.LicenseNumber == userDto.LicenseNumber).FirstOrDefault();
            if (currentUserFromDB == null)
            {
                // If no user found with the given license number, return NotFound
                return NotFound("User not found.");
            }
            if (!modelsUser.FirstName.IsNullOrEmpty())
            {
                currentUserFromDB.FirstName = modelsUser.FirstName;
            }
            if (!modelsUser.LastName.IsNullOrEmpty())
            {
                currentUserFromDB.LastName = modelsUser.LastName;
            }
            if (!modelsUser.Email.IsNullOrEmpty())
            {
                currentUserFromDB.Email = modelsUser.Email;
            }
            if (!modelsUser.UserPassword.IsNullOrEmpty())
            {
                currentUserFromDB.UserPassword = modelsUser.UserPassword;
            }
            context.SaveChanges();
            MyGarageFinderServer.DTO.UserDTO updatetDtoUser = new MyGarageFinderServer.DTO.UserDTO(currentUserFromDB);
            return Ok(updatetDtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    #region GarageReader

    // פעולה חדשה להוספת נתונים מה-API
    [HttpPost("import")]
    public async Task<IActionResult> ImportGarages()
    {
        try
        {
            // קריאה לשירות שיבצע את הייבוא מה-API
            await _garageService.ImportGaragesFromApiAsync();

            // החזרת הודעת הצלחה
            return Ok("Data Imported Successfully!");
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה, החזרת הודעת שגיאה
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    #endregion


    [HttpGet("getallspecialization")]
    public IActionResult GetAllSpecializations()
    {
        try
        {
            List<GarageSpecialization> specs = new List<GarageSpecialization>();
            foreach (Garage g in context.Garages)
            {
                GarageSpecialization spec = new GarageSpecialization();
                spec.Specialization = g.Specialization;
                spec.SpecializationCode = (int)g.SpecializationCode;
                specs.Add(spec);
            }
            int Maxcode = 0;
            foreach (GarageSpecialization s in specs)
            {
                Maxcode = Math.Max(Maxcode, s.SpecializationCode);
            }
            List<GarageSpecialization> newSpecs = new List<GarageSpecialization>();
            for (int i = 0; i <= Maxcode; i++)
            {
                newSpecs.Add((GarageSpecialization)specs.Where(u => u.SpecializationCode == i).FirstOrDefault());
            }
            List<GarageSpecialization> Gspecs = new List<GarageSpecialization>();
            foreach (GarageSpecialization g in newSpecs)
            {
                if (g != null)
                {
                    Gspecs.Add(g);
                }
            }
            return Ok(Gspecs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("getallgarages")]
    public IActionResult GetAllGarages()
    {
        try
        {
            List<GarageDTO> garages = new List<GarageDTO>();
            List<int?> garageSpecializations = new List<int?>();
            GarageDTO garage = new GarageDTO();
            int currentLicense = 0; bool sameGarage = false;
            foreach (Garage g in context.Garages)
            {
                if (g.GarageLicense != currentLicense)
                {
                    if (currentLicense != 0)
                    {
                        garage.GarageSpecs = garageSpecializations;
                        garages.Add(garage);
                    }
                    garageSpecializations = new List<int?>();
                    garage = new GarageDTO(g);
                    currentLicense = g.GarageLicense;
                    garageSpecializations.Add(g.SpecializationCode);
                }
                else
                {
                    garageSpecializations.Add(g.SpecializationCode);
                }
            }
            garage.GarageSpecs = garageSpecializations;
            garages.Add(garage);
            return Ok(garages);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("geteachonegarageimage")]
    public IActionResult GetGaragesImages([FromBody] int[] garagesId)
    {
        try
        {
            List<GarageImageDTO> garagesImages = new List<GarageImageDTO>();
            foreach (int id in garagesId)
            {
                GarageImage img = context.GarageImages.Where(u => u.GarageId == id).FirstOrDefault();
                GarageImageDTO iDTO = new GarageImageDTO
                {
                    GarageID = img.GarageId,
                    GarageImageID = img.GarageImageId,
                    ImageURL = img.ImageUrl
                };
                garagesImages.Add(iDTO);
            }
            return Ok(garagesImages);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("getallreviews")]
    public IActionResult GetAllReviews()
    {
        try
        {
            List<ReviewDTO> reviews = new List<ReviewDTO>();
            foreach (Review r in context.Reviews)
            {
                ReviewDTO rdto = new ReviewDTO
                {
                    ReviewID = r.ReviewId,
                    Rating = r.Rating,
                    ReviewDescription = r.ReviewDescription,
                    UserID = r.UserId,
                    GarageID = r.GarageId
                };
                reviews.Add(rdto);
            }
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("addreview")]
    public IActionResult RegisterReview([FromBody] MyGarageFinderServer.DTO.ReviewDTO rDTO)
    {
        try
        {

            MyGarageFinderServer.Models.Review reviewModel = new Review
            {
                ReviewDescription = rDTO.ReviewDescription,
                Rating = rDTO.Rating,
                UserId = rDTO.UserID,
                GarageId = rDTO.GarageID,
                ReviewTimestamp = rDTO.ReviewTimestamp,
            };
            
            context.Reviews.Add(reviewModel);
            context.SaveChanges();

            return Ok(reviewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




}
