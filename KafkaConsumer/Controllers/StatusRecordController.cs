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
    public class StatusRecordController : ControllerBase
    {
        private readonly IStatusRecordRepository _recordRepository;

        public StatusRecordController(IStatusRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }
        // GET: api/<StatusRecordController>
        [HttpGet]
        public IActionResult Get()
        {
            var records = _recordRepository.GetAll();
            return records == null ? NotFound() : Ok(records);
        }

        // GET api/<StatusRecordController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var record = _recordRepository.GetById(id);
            return record == null ? NotFound() : Ok(record);
        }

        [HttpGet("/machines/{machineId}/all-records")]
        public IActionResult GetAllMachineRecord(Guid machineId)
        {
            var records = _recordRepository.Find(r=>r.MachineId==machineId);
            return records == null ? NotFound() : Ok(records);
        }



        // DELETE api/<StatusRecordController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var recordStatus = _recordRepository.GetById(id);
            if (recordStatus == null)
            {
                return NotFound();
            }
            else
            {
                _recordRepository.Remove(recordStatus);
                return Ok("record status is removed succeffuly");
            }
        }
    }
}
