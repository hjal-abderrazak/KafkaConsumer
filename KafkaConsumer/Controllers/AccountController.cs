
using KafkaConsumer.Dtos;
using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository r)
        {
            _userRepository = r;
        }


        [HttpPost("register")]
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),

                // Add any additional claims here...
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           var user = _userRepository.Find(u=>u.Email == loginDto.Email).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("email  is wrong");
            }
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, user.Password, loginDto.Password);
            switch (result)
            {
                case PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded:
                    // Password is correct
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.GivenName, user.FirstName),
                        new Claim(ClaimTypes.Surname, user.LastName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    Response.Headers.Add("X-Authenticated", "true");
                    return Ok();
                    break;
                case PasswordVerificationResult.Failed:
                    // Password is incorrect
                    return BadRequest("password is wrong");
                    break;
                default:
                    return BadRequest("something  wrong");
                    break;
            }


            
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> profile()
        {
            Response.Headers.Add("X-Authenticated", "true");
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            return Ok(_userRepository.GetById(Guid.Parse(userId)));
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
