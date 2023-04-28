using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository=departmentRepository;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentRepository.GetAll();
            return departments == null ? NotFound() : Ok(departments);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(Guid id)
        {
            var department = _departmentRepository.GetById(id);
            return department == null ? NotFound(): Ok(department) ;
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            if (department.Name.IsNullOrEmpty())
            {
                return BadRequest("name is required ");
            }
            _departmentRepository.Add(department);
            return Ok("department added succefuly");
        }

        // PUT api/<DepartmentController>/5
        [HttpPut()]
        public IActionResult update([FromBody] Department department)
        {
            if (department.Name.IsNullOrEmpty()) {
                return BadRequest("name is required ");
            }
            else
            {
                var departmentToUpdate = _departmentRepository.GetById(department.Id);
                if (departmentToUpdate != null)
                    _departmentRepository.Update(department);
                else
                    return BadRequest("departmanet not found");

            }
            return Ok("department updated succefully");
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                _departmentRepository.Remove(department);
                return Ok("department is removed succeffuly");
            }
        }

    }
}
