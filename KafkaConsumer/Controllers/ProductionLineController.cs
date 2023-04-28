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
    public class ProductionLineController : ControllerBase
    {
        private readonly IProductionLineRepository _prodductionLineRepository;

        public ProductionLineController(IProductionLineRepository pr)
        {
            _prodductionLineRepository = pr;
        }
        // GET: api/<ProductionLineController>
        [HttpGet]
        public IActionResult Get()
        {
            var productionLines = _prodductionLineRepository.GetAll();
            return productionLines == null ? NotFound() : Ok(productionLines);
        }

        // GET api/<ProductionLineController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var productionLine = _prodductionLineRepository.GetById(id);
            return productionLine == null ? NotFound() : Ok(productionLine);
        }

        // POST api/<ProductionLineController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductionLine productionLine)
        {
            if (productionLine.Name.IsNullOrEmpty())
            {
                return BadRequest("name is required ");
            }
            _prodductionLineRepository.Add(productionLine);
            return Ok("production line added succefuly");
        }

        // PUT api/<ProductionLineController>/5
        [HttpPut("{id}")]
        public IActionResult update([FromBody] ProductionLine productionLine)
        {
            var productionLineToUpdate = _prodductionLineRepository.GetById(productionLine.Id);
            if (productionLineToUpdate != null)
                _prodductionLineRepository.Update(productionLine);
            else
                return BadRequest("production line not found");

            return Ok("production line updated succefully");
        }

        // DELETE api/<ProductionLineController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var productionLine = _prodductionLineRepository.GetById(id);
            if (productionLine == null)
            {
                return NotFound();
            }
            else
            {
                _prodductionLineRepository.Remove(productionLine);
                return Ok("production line is removed succeffuly");
            }
        }
    }
}
