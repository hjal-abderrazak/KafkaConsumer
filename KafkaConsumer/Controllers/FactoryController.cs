using KafkaConsumer.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryRepository _factoryRepository;

        public FactoryController(IFactoryRepository factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }
        // GET: api/<FactoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FactoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FactoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FactoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FactoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
