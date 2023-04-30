using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userRepository.GetAll();
            return users == null ? NotFound() : Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var user = _userRepository.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user.Email.IsNullOrEmpty())
            {
                return BadRequest("email is required ");
            }
           
            _userRepository.Add(user);
            return Ok("user added succefuly");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult update([FromBody] User user)
        {
            var userToUpdate = _userRepository.GetById(user.Id);
            if (userToUpdate != null)
                _userRepository.Update(user);
            else
                return BadRequest("user not found");

            return Ok("user updated succefully");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _userRepository.Remove(user);
                return Ok("user is removed succeffuly");
            }
        }
    }
}
