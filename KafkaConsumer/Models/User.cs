using System.Text.Json.Serialization;

namespace KafkaConsumer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid? FactoryId { get; set; }
        public Factory? Factory { get; set; }
        public ICollection<Maintenance> Maintenances { get; }= new List<Maintenance>();
        public User()
        {
            
        }

    }
}
