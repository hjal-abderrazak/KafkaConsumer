using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController( IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository; 
        }
        // GET: api/<MachineController>
        [HttpGet]
        public IActionResult Get()
        {
            var machines = _machineRepository.GetAll();
            return machines == null ? NotFound() : Ok(machines);
        }

        // GET api/<MachineController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var machine = _machineRepository.GetById(id);
            return machine == null ? NotFound() : Ok(machine);
        }

        // POST api/<MachineController>
        [HttpPost]
        public IActionResult Post([FromBody] Machine machine)
        {
            if (machine.StartDate==null || machine.Name.IsNullOrEmpty())
            {
                return BadRequest("start date and name are required ");
            }
            _machineRepository.Add(machine);
            return Ok("machine added succefuly");
        }

        // PUT api/<MachineController>/5
        [HttpPut("{id}")]
        public IActionResult update([FromBody] Machine machine)
        {
            var machineToUpdate = _machineRepository.GetById(machine.Id);
            if (machineToUpdate != null)
                _machineRepository.Update(machine);
            else
                return BadRequest("machine not found");

            return Ok("machine updated succefully");
        }

        // DELETE api/<MachineController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var machine = _machineRepository.GetById(id);
            if (machine == null)
            {
                return NotFound();
            }
            else
            {
                _machineRepository.Remove(machine);
                return Ok("machine is removed succeffuly");
            }
        }
    }
}
