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
            foreach(Vehicle v in userVehicles)
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




}
