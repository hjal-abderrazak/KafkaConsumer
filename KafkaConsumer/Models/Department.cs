using Microsoft.Extensions.Hosting;

namespace KafkaConsumer.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; } = new List<User>(); // Collection of employer in a department 

        public Department()
        {
            
        }
    }
}
