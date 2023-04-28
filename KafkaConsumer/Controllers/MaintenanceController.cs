using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceController(IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }
        // GET: api/<MaintenanceController>
        [HttpGet]
        public IActionResult Get()
        {
            var maintenances = _maintenanceRepository.GetAll();
            return maintenances == null ? NotFound() : Ok(maintenances);
        }

        // GET api/<MaintenanceController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var maintenance = _maintenanceRepository.GetById(id);
            return maintenance == null ? NotFound() : Ok(maintenance);
        }

        // POST api/<MaintenanceController>
        [HttpPost]
        public IActionResult Post([FromBody] Maintenance maintenance)
        {
            if (maintenance.MaintainedTime==new DateTime())
            {
                return BadRequest(" maintained date is required ");
            }
            _maintenanceRepository.Add(maintenance);
            return Ok("maintenance added succefuly");
        }
    

        // PUT api/<MaintenanceController>/5
        [HttpPut("{id}")]
        public IActionResult update([FromBody] Maintenance maintenance)
        {
            var maintenanceToUpdate = _maintenanceRepository.GetById(maintenance.Id);
            if (maintenanceToUpdate != null)
                _maintenanceRepository.Update(maintenance);
            else
                return BadRequest("maintenance not found");

            return Ok("maintenance updated succefully");
        }
        // DELETE api/<MaintenanceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var maintenance = _maintenanceRepository.GetById(id);
            if (maintenance == null)
            {
                return NotFound();
            }
            else
            {
                _maintenanceRepository.Remove(maintenance);
                return Ok("maintenance is removed succeffuly");
            }
        }
    }
}
