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
    [Authorize]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryRepository _factoryRepository;

        public FactoryController(IFactoryRepository factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }
        // GET: api/<FactoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var factories = _factoryRepository.GetAll();
            return factories == null ? NotFound() : Ok(factories);
        }

        // GET api/<FactoryController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var factory = _factoryRepository.GetById(id);
            return factory == null ? NotFound() : Ok(factory);
        }

        // POST api/<FactoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Factory factory)
        {
            if (factory.Name==null || factory.Reference==null)
            {
                return BadRequest("name and refenence are required ");
            }
            _factoryRepository.Add(factory);
            return Ok("factory added succefuly");
        }

        // PUT api/<FactoryController>/5
        [HttpPut("{id}")]
        public IActionResult update([FromBody] Factory factory)
        {
            var factoryToUpdate = _factoryRepository.GetById(factory.Id);
            if (factoryToUpdate != null)
                _factoryRepository.Update(factory);
            else
                return BadRequest("factory not found");

            return Ok("factory updated succefully");
        }

        // DELETE api/<FactoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var factory = _factoryRepository.GetById(id);
            if (factory == null)
            {
                return NotFound();
            }
            else
            {
                _factoryRepository.Remove(factory);
                return Ok("factory is removed succeffuly");
            }
        }
    }
}
