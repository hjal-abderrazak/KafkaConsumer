
using Confluent.Kafka;
using KafkaConsumer.Dtos;
using KafkaConsumer.Helper;
using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper jwtHelper;
        public IConfiguration config { get; }

        public AccountController(IUserRepository r,IConfiguration configuration,JwtHelper helper)
        {
            _userRepository = r;
            config = configuration;
            jwtHelper = helper;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto userDto)
        {
            
            var passwordHasher = new PasswordHasher<object>();
            var user = new User 
            { Email = userDto.Email,
            FirstName=userDto.FirstName,
            LastName = userDto.LastName,
            Password= passwordHasher.HashPassword(null, userDto.Password),
            Role = userDto.Role};
            _userRepository.Add(user);            
            return Ok(new {message = "account created succefuly"});
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           var user = _userRepository.Find(u=>u.Email == loginDto.Email).FirstOrDefault();
            if (user == null)
            {
                return BadRequest(new{message = "sorry we have not found accound with this email"});
            }
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, user.Password, loginDto.Password);
            switch (result)
            {
                case PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded:
                    // Password is correct
                    var token = jwtHelper.generate(user); // generate JWT token here
                    Response.Cookies.Append("token", token, 
                        new CookieOptions {IsEssential=true, HttpOnly = true,SameSite=SameSiteMode.None,Secure=true });
                    return Ok(new { message = "success", token = token });
                    break;
                case PasswordVerificationResult.Failed:
                    // Password is incorrect
                    return BadRequest(new{message = "password incoorect"});
                    break;
                default:
                    return BadRequest("something  wrong");
                    break;
            }


            
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token");
            return Ok(new { message = "Logout successful" });
        }

        [HttpGet]
        [Route("profile")]
        //[Authorize]
        public async Task<IActionResult> profile()
        {   //method 1 
            var jwt = Request.Cookies["token"];
            var token = jwtHelper.Verify(jwt);
            var claims = token.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userId == null)
            {
                Unauthorized();
            }
            return Ok(_userRepository.GetById(Guid.Parse(userId)));

            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //if(identity != null)
            //{
            //    var userClaims = identity.Claims;
            //    var user = new User
            //    {
            //        Id = Guid.Parse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value),
            //        FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
            //        Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
            //        LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
            //        Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            //    };
            //    return Ok(user);
            //}
            //return BadRequest(new { Message = "error" });

        }



        
    }
}

   


/*
ClaimTypes.Name: The user's name.
ClaimTypes.NameIdentifier: A unique identifier for the user.
ClaimTypes.Role: The user's role(s) or permission(s).
ClaimTypes.Email: The user's email address.
ClaimTypes.GivenName: The user's given name (first name).
ClaimTypes.Surname: The user's surname (last name).
ClaimTypes.DateOfBirth: The user's date of birth.
ClaimTypes.Country: The user's country.
ClaimTypes.StateOrProvince: The user's state or province.
ClaimTypes.PostalCode: The user's postal code.
ClaimTypes.MobilePhone: The user's mobile phone number.
ClaimTypes.HomePhone: The user's home phone number.
ClaimTypes.StreetAddress: The user's street address.
ClaimTypes.City: The user's city.
 
 */
