namespace KafkaConsumer.Models
{
    public class Maintenance
    {
        public Guid Id { get; set; }
        public DateTime MaintainedTime { get; set; }
        public string Comment { get; set; }

        public Guid TechnicienId { get; set; }
        public User Technicien { get; set; }
        public Guid MachineId { get; set; }
        public Machine Machine { get; set; }


        public Maintenance()
        {
            
        }
    }
}
