using Microsoft.AspNetCore.Mvc;
using MyGarageFinderServer.Models;

[Route("api")]
[ApiController]
public class MyGarageFinderAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private MyGarageFinderDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    public MyGarageFinderAPIController(MyGarageFinderDbContext context, IWebHostEnvironment env)
    {
        this.context = context;
        this.webHostEnvironment = env;
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

}
