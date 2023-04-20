using KafkaConsumer.DAL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumerController( AppDbContext context)
        {
            _context = context;  
        }

       
    }
}
